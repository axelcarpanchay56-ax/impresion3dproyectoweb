using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpresionController : ControllerBase
    {
        private readonly ServicioModelo3D _modeloServicio;

        public ImpresionController(ServicioModelo3D modeloServicio)
        {
            _modeloServicio = modeloServicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelos = await _modeloServicio.ObtenerTodosAsync();
            var dtos = modelos.Select(m => new Modelo3DDto
            {
                id_modelo3d = m.id_modelo3d,
                nombre = m.nombre,
                archivo = m.archivo,
                formato = m.formato,
                descripcion = m.descripcion,
                fecha_creacion = m.fecha_creacion
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Modelo3DCrearDto dto)
        {
            var entidad = new Modelo3D
            {
                nombre = dto.nombre,
                archivo = dto.archivo,
                formato = dto.formato,
                descripcion = dto.descripcion,
                fecha_creacion = DateTime.Now
            };

            var id = await _modeloServicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }
    }
}
