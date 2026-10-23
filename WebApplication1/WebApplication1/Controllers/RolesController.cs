using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ServicioRol _servicio;

        public RolesController(ServicioRol servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _servicio.ObtenerTodosAsync();

            var rolesDto = roles.Select(rol => new RolDto
            {
                IdRol = rol.IdRol,
                Nombre = rol.nombre,
                Descripcion = rol.descripcion
            }).ToList();

            return Ok(rolesDto);
        }
    }
}  