using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ServicioUsuario _servicio;

        public UsuariosController(ServicioUsuario servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            var usuario = await _servicio.LoginAsync(
                dto.Usuario,
                dto.PasswordHash
            );

            if (usuario == null)
            {
                return Unauthorized(
                    "Usuario o contraseña incorrectos."
                );
            }

            var usuarioDto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                IdPersona = usuario.id_persona,
                Usuario = usuario.usuario,
                Activo = usuario.activo,
                FechaCreacion = usuario.fecha_creacion,
                UltimoAcceso = usuario.ultimo_acceso
            };

            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearUsuarioDto dto)
        {
            var usuario = new Usuarios
            {
                id_persona = dto.IdPersona,
                usuario = dto.Usuario,
                password_hash = dto.PasswordHash,
                activo = true,
                fecha_creacion = DateTime.Now,
                ultimo_acceso = null
            };

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

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(
            int id,
            bool activo)
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
    }
}