using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioRolController : ControllerBase
    {
        private readonly ServicioUsuarioRol _servicio;

        public UsuarioRolController(ServicioUsuarioRol servicio)
        {
            _servicio = servicio;
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(
            AsignarUsuarioRolDto dto)
        {
            var usuarioRol = new Usuario_Rol
            {
                id_usuario = dto.IdUsuario,
                id_rol = dto.IdRol
            };

            int resultado =
                await _servicio.AsignarAsync(usuarioRol);

            if (resultado <= 0)
            {
                return BadRequest(
                    "No se pudo asignar el rol al usuario."
                );
            }

            return Ok(
                "Rol asignado correctamente."
            );
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario(
            int idUsuario)
        {
            var roles =
                await _servicio.ObtenerPorUsuarioAsync(idUsuario);

            var rolesDto = roles.Select(rol => new UsuarioRolDto
            {
                IdUsuario = rol.id_usuario,
                IdRol = rol.id_rol
            }).ToList();

            return Ok(rolesDto);
        }
    }
} 