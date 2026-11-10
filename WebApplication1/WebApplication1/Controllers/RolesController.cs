using AutoMapper;
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
        private readonly IMapper _mapper;

        public RolesController(
            ServicioRol servicio,
            IMapper mapper)
        {
            _servicio = servicio;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _servicio.ObtenerTodosAsync();

            var rolesDto =
                _mapper.Map<List<RolDto>>(roles);

            return Ok(rolesDto);
        }
    }
}
