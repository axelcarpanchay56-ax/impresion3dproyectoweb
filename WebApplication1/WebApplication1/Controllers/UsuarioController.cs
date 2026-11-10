using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ServicioUsuario _servicio;
        private readonly ServicioPersona _servicioPersona;
        private readonly ServicioCliente _servicioCliente;
        private readonly ServicioRol _servicioRol;
        private readonly ServicioUsuarioRol _servicioUsuarioRol;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IAutenticacionService _autenticacionService;
        private readonly IValidator<CrearUsuarioDto> _validatorCrearUsuario;
        private readonly IValidator<RegistroUsuarioDto> _validatorRegistroUsuario;
        private readonly IMapper _mapper;

        public UsuariosController(
            ServicioUsuario servicio,
            ServicioPersona servicioPersona,
            ServicioCliente servicioCliente,
            ServicioRol servicioRol,
            ServicioUsuarioRol servicioUsuarioRol,
            IUsuarioRepositorio usuarioRepositorio,
            IAutenticacionService autenticacionService,
            IValidator<CrearUsuarioDto> validatorCrearUsuario,
            IValidator<RegistroUsuarioDto> validatorRegistroUsuario,
            IMapper mapper)
        {
            _servicio = servicio;
            _servicioPersona = servicioPersona;
            _servicioCliente = servicioCliente;
            _servicioRol = servicioRol;
            _servicioUsuarioRol = servicioUsuarioRol;
            _usuarioRepositorio = usuarioRepositorio;
            _autenticacionService = autenticacionService;
            _validatorCrearUsuario = validatorCrearUsuario;
            _validatorRegistroUsuario = validatorRegistroUsuario;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            var usuario =
                await _autenticacionService.ValidarCredencialesAsync(
                    dto.Usuario,
                    dto.PasswordHash
                );

            if (usuario == null)
            {
                return Unauthorized(
                    "Usuario o contraseña incorrectos."
                );
            }

            var roles =
                await _autenticacionService.ObtenerRolesAsync(
                    usuario.IdUsuario
                );

            var token =
                _autenticacionService.GenerarToken(
                    usuario,
                    roles
                );

            bool esAdmin = roles.Any(r =>
                string.Equals(r, "Administrador", StringComparison.OrdinalIgnoreCase)
            );

            return Ok(new
            {
                Token = token,
                Usuario = usuario.usuario,
                Roles = roles,
                EsAdmin = esAdmin
            });
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar(RegistroUsuarioDto dto)
        {
            var validationResultado =
                await _validatorRegistroUsuario.ValidateAsync(dto);

            if (!validationResultado.IsValid)
            {
                var errores = validationResultado.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Mensaje = "Error de validación",
                    Errores = errores
                });
            }

            try
            {
                var usuarioExistente =
                    await _usuarioRepositorio.ObtenerPorUsuarioAsync(dto.Usuario.Trim());

                if (usuarioExistente != null)
                {
                    return BadRequest(new
                    {
                        Mensaje = "El nombre de usuario ya está registrado."
                    });
                }

                var persona = _mapper.Map<Persona>(dto);
                persona.Activo = true;
                persona.FechaAlta = DateTime.Now;

                int filasPersona = await _servicioPersona.CrearAsync(persona);
                if (filasPersona <= 0)
                {
                    return BadRequest(new
                    {
                        Mensaje = "No se pudo registrar los datos personales."
                    });
                }

                var personaCreada = await _servicioPersona.GetByDniAsync(dto.Dni.Trim());
                if (personaCreada == null)
                {
                    return BadRequest(new
                    {
                        Mensaje = "Error al recuperar la persona registrada."
                    });
                }

                await _servicioCliente.CrearAsync(new Clientes
                {
                    id_persona = personaCreada.IdPersona
                });

                var nuevoUsuario = new Usuarios
                {
                    id_persona = personaCreada.IdPersona,
                    usuario = dto.Usuario.Trim(),
                    password_hash = dto.Password,
                    activo = true,
                    fecha_creacion = DateTime.Now,
                    ultimo_acceso = DateTime.Now
                };

                int filasUsuario = await _servicio.CrearAsync(nuevoUsuario);
                if (filasUsuario <= 0)
                {
                    return BadRequest(new
                    {
                        Mensaje = "No se pudo crear la cuenta de usuario."
                    });
                }

                var usuarioCreado =
                    await _usuarioRepositorio.ObtenerPorUsuarioAsync(dto.Usuario.Trim());

                var rolesAsignados = new List<string>();

                if (usuarioCreado != null)
                {
                    var todosLosRoles = await _servicioRol.ObtenerTodosAsync();
                    var rolCliente = todosLosRoles.FirstOrDefault(r =>
                        string.Equals(r.nombre, "Cliente", StringComparison.OrdinalIgnoreCase)
                    );

                    if (rolCliente != null)
                    {
                        await _servicioUsuarioRol.AsignarAsync(new Usuario_Rol
                        {
                            id_usuario = usuarioCreado.IdUsuario,
                            id_rol = rolCliente.IdRol
                        });
                        rolesAsignados.Add(rolCliente.nombre);
                    }

                    var token = _autenticacionService.GenerarToken(
                        usuarioCreado,
                        rolesAsignados
                    );

                    return Ok(new
                    {
                        Mensaje = "Usuario registrado correctamente.",
                        Token = token,
                        Usuario = usuarioCreado.usuario,
                        NombreCompleto = $"{personaCreada.Nombre} {personaCreada.Apellido}".Trim(),
                        Roles = rolesAsignados,
                        EsAdmin = false
                    });
                }

                return Ok(new
                {
                    Mensaje = "Usuario registrado correctamente."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            try
            {
                string email = dto.Email?.Trim() ?? string.Empty;
                string nombre = dto.Nombre?.Trim() ?? string.Empty;
                string apellido = dto.Apellido?.Trim() ?? string.Empty;
                string googleSub = dto.GoogleId?.Trim() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(dto.Credential))
                {
                    var handler = new JwtSecurityTokenHandler();
                    if (handler.CanReadToken(dto.Credential))
                    {
                        var jwt = handler.ReadJwtToken(dto.Credential);
                        email = jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? email;
                        nombre = jwt.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value ?? nombre;
                        apellido = jwt.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value ?? apellido;
                        googleSub = jwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? googleSub;

                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            var fullName = jwt.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "";
                            var parts = fullName.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length > 0) nombre = parts[0];
                            if (parts.Length > 1) apellido = parts[1];
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                {
                    return BadRequest(new
                    {
                        Mensaje = "Se requiere un correo de Google válido."
                    });
                }

                var baseUser = email.Split('@')[0].Trim();
                if (baseUser.Length > 45) baseUser = baseUser.Substring(0, 45);
                if (string.IsNullOrWhiteSpace(nombre)) nombre = baseUser;
                if (string.IsNullOrWhiteSpace(apellido)) apellido = "Google";

                var usuarioExistente =
                    await _usuarioRepositorio.ObtenerPorUsuarioAsync(baseUser);

                if (usuarioExistente != null)
                {
                    var roles = await _autenticacionService.ObtenerRolesAsync(usuarioExistente.IdUsuario);
                    var token = _autenticacionService.GenerarToken(usuarioExistente, roles);
                    bool esAdmin = roles.Any(r =>
                        string.Equals(r, "Administrador", StringComparison.OrdinalIgnoreCase)
                    );

                    return Ok(new
                    {
                        Token = token,
                        Usuario = usuarioExistente.usuario,
                        NombreCompleto = $"{nombre} {apellido}".Trim(),
                        Email = email,
                        Roles = roles,
                        EsAdmin = esAdmin
                    });
                }

                // Verificar si ya existe la persona por email, o crear una nueva en la BD
                var todasPersonas = await _servicioPersona.AllAsync();
                var personaExistente = todasPersonas.FirstOrDefault(p =>
                    string.Equals(p.Email, email, StringComparison.OrdinalIgnoreCase)
                );

                if (personaExistente == null)
                {
                    int hashNum = Math.Abs((email + googleSub).GetHashCode() % 89999999) + 10000000;
                    string dniGenerado = hashNum.ToString();

                    while (await _servicioPersona.GetByDniAsync(dniGenerado) != null)
                    {
                        hashNum = (hashNum + 1) % 89999999 + 10000000;
                        dniGenerado = hashNum.ToString();
                    }

                    var nuevaPersona = new Persona
                    {
                        Dni = dniGenerado,
                        Nombre = nombre,
                        Apellido = apellido,
                        Email = email,
                        Telefono = null,
                        Direccion = "Cuenta Google",
                        Activo = true,
                        FechaAlta = DateTime.Now
                    };

                    await _servicioPersona.CrearAsync(nuevaPersona);
                    personaExistente = await _servicioPersona.GetByDniAsync(dniGenerado);
                }

                if (personaExistente == null)
                {
                    return BadRequest(new
                    {
                        Mensaje = "No se pudo registrar la persona vinculada a Google."
                    });
                }

                var clientesActuales = await _servicioCliente.ObtenerTodosAsync();
                if (!clientesActuales.Any(c => c.id_persona == personaExistente.IdPersona))
                {
                    await _servicioCliente.CrearAsync(new Clientes
                    {
                        id_persona = personaExistente.IdPersona
                    });
                }

                var nuevoUsuario = new Usuarios
                {
                    id_persona = personaExistente.IdPersona,
                    usuario = baseUser,
                    password_hash = $"GoogleAuth_{Guid.NewGuid():N}",
                    activo = true,
                    fecha_creacion = DateTime.Now,
                    ultimo_acceso = DateTime.Now
                };

                await _servicio.CrearAsync(nuevoUsuario);

                var usuarioCreado =
                    await _usuarioRepositorio.ObtenerPorUsuarioAsync(baseUser);

                var rolesAsignados = new List<string>();
                if (usuarioCreado != null)
                {
                    var todosLosRoles = await _servicioRol.ObtenerTodosAsync();
                    var rolCliente = todosLosRoles.FirstOrDefault(r =>
                        string.Equals(r.nombre, "Cliente", StringComparison.OrdinalIgnoreCase)
                    );

                    if (rolCliente != null)
                    {
                        await _servicioUsuarioRol.AsignarAsync(new Usuario_Rol
                        {
                            id_usuario = usuarioCreado.IdUsuario,
                            id_rol = rolCliente.IdRol
                        });
                        rolesAsignados.Add(rolCliente.nombre);
                    }

                    var token = _autenticacionService.GenerarToken(usuarioCreado, rolesAsignados);

                    return Ok(new
                    {
                        Mensaje = "Sesión iniciada con Google y guardada en la base de datos.",
                        Token = token,
                        Usuario = usuarioCreado.usuario,
                        NombreCompleto = $"{personaExistente.Nombre} {personaExistente.Apellido}".Trim(),
                        Email = email,
                        Roles = rolesAsignados,
                        EsAdmin = false
                    });
                }

                return BadRequest(new
                {
                    Mensaje = "No se pudo completar el inicio de sesión con Google."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearUsuarioDto dto)
        {
            var validationResultado =
                await _validatorCrearUsuario.ValidateAsync(dto);

            if (!validationResultado.IsValid)
            {
                var errores = validationResultado.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Mensaje = "Error de validación",
                    Errores = errores
                });
            }

            try
            {
                var usuario =
                    _mapper.Map<Usuarios>(dto);

                usuario.activo = true;
                usuario.fecha_creacion = DateTime.Now;
                usuario.ultimo_acceso = null;

                int resultado =
                    await _servicio.CrearAsync(usuario);

                if (resultado <= 0)
                {
                    return BadRequest(
                        "No se pudo crear el usuario."
                    );
                }

                return Ok(
                    "Usuario creado correctamente."
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(
            int id,
            bool activo)
        {
            try
            {
                bool resultado =
                    await _servicio.CambiarEstadoAsync(
                        id,
                        activo
                    );

                if (!resultado)
                {
                    return BadRequest(
                        "No se pudo cambiar el estado del usuario."
                    );
                }

                return Ok(
                    "Estado del usuario actualizado correctamente."
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("protegido")]
        public IActionResult Protegido()
        {
            return Ok("Acceso autorizado.");
        }
    }
}
