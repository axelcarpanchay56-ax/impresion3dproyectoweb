using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly ServicioMateriales _servicio;

        public MaterialesController(ServicioMateriales servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var materiales = await _servicio.ObtenerTodosAsync();
            var dtos = materiales.Select(m => new MaterialesDto
            {
                id_material = m.id_material,
                nombre = m.nombre,
                tipo = m.tipo,
                unidad_medida = m.unidad_medida,
                color = m.color,
                diametro = m.diametro,
                activo = m.activo
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var m = await _servicio.ObtenerPorIdAsync(id);
            var dto = new MaterialesDto
            {
                id_material = m.id_material,
                nombre = m.nombre,
                tipo = m.tipo,
                unidad_medida = m.unidad_medida,
                color = m.color,
                diametro = m.diametro,
                activo = m.activo
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MaterialesCrearDto dto)
        {
            var entidad = new Materiales
            {
                nombre = dto.nombre,
                tipo = dto.tipo,
                unidad_medida = dto.unidad_medida,
                color = dto.color,
                diametro = dto.diametro,
                activo = dto.activo
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaterialesActualizarDto dto)
        {
            var entidad = new Materiales
            {
                id_material = id,
                nombre = dto.nombre,
                tipo = dto.tipo,
                unidad_medida = dto.unidad_medida,
                color = dto.color,
                diametro = dto.diametro,
                activo = dto.activo
            };

            await _servicio.ActualizarAsync(entidad);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicio.EliminarAsync(id);
            return NoContent();
        }
    }
}
