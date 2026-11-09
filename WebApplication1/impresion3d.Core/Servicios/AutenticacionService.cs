using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace impresion3d.Core.Servicios
{
    public class AutenticacionService : IAutenticacionService
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IUsuarioRolRepositorio _usuarioRolRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IConfiguration _configuration;

        public AutenticacionService(
            IUsuarioRepositorio repositorio,
            IUsuarioRolRepositorio usuarioRolRepositorio,
            IRolRepositorio rolRepositorio,
            IConfiguration configuration)
        {
            _repositorio = repositorio;
            _usuarioRolRepositorio = usuarioRolRepositorio;
            _rolRepositorio = rolRepositorio;
            _configuration = configuration;
        }

        public async Task<Usuarios?> ValidarCredencialesAsync(
            string usuario,
            string password)
        {
            var usuarioEncontrado =
                await _repositorio.ObtenerPorUsuarioAsync(usuario);

            if (usuarioEncontrado == null)
            {
                return null;
            }

            if (!usuarioEncontrado.activo)
            {
                return null;
            }

            bool passwordCorrecto =
                ServicioPassword.VerificarPassword(
                    password,
                    usuarioEncontrado.password_hash
                );

            if (!passwordCorrecto)
            {
                return null;
            }

            return usuarioEncontrado;
        }

        public async Task<List<string>> ObtenerRolesAsync(int idUsuario)
        {
            var relaciones =
                await _usuarioRolRepositorio.ObtenerPorUsuarioAsync(idUsuario);

            var todosLosRoles =
                await _rolRepositorio.ObtenerTodosAsync();

            var nombresRoles = new List<string>();

            foreach (var relacion in relaciones)
            {
                var rol = todosLosRoles
     .FirstOrDefault(r => r.IdRol == relacion.id_rol); 

                if (rol != null)
                {
                    nombresRoles.Add(rol.nombre);
                }
            }

            return nombresRoles;
        }

        public string GenerarToken(
            Usuarios usuario,
            List<string> roles)
        {
            var llaveSecreta =
                _configuration["Jwt:llave_secreta"];

            var emisor =
                _configuration["Jwt:emisor"];

            var audiencia =
                _configuration["Jwt:audiencia"];

            var tiempoExpiracion =
                int.Parse(
                    _configuration[
                        "Jwt:tiempo_expiracion_minutos"
                    ] ?? "180"
                );

            var claveSeguridad =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        llaveSecreta!
                    )
                );

            var credenciales =
                new SigningCredentials(
                    claveSeguridad,
                    SecurityAlgorithms.HmacSha256
                );

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.IdUsuario.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    usuario.usuario
                )
            };

            foreach (var rol in roles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        rol
                    )
                );
            }

            var token =
                new JwtSecurityToken(
                    issuer: emisor,
                    audience: audiencia,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(
                        tiempoExpiracion
                    ),
                    signingCredentials: credenciales
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
} 