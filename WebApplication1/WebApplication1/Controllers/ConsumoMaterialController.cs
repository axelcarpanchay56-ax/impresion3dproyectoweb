using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumoMaterialController : ControllerBase
    {
        private readonly ServicioConsumoMaterial _servicio;

        public ConsumoMaterialController(ServicioConsumoMaterial servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var consumos = await _servicio.ObtenerTodosAsync();
            var dtos = consumos.Select(c => new ConsumoMaterialDto
            {
                id_consumo_material = c.id_consumo_material,
                id_trabajo = c.id_trabajo,
                id_material = c.id_material,
                cantidad = c.cantidad,
                fecha_hora = c.fecha_hora
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _servicio.ObtenerPorIdAsync(id);
            var dto = new ConsumoMaterialDto
            {
                id_consumo_material = c.id_consumo_material,
                id_trabajo = c.id_trabajo,
                id_material = c.id_material,
                cantidad = c.cantidad,
                fecha_hora = c.fecha_hora
            };
            return Ok(dto);
        }

        [HttpGet("trabajo/{id_trabajo}")]
        public async Task<IActionResult> GetByTrabajo(int id_trabajo)
        {
            var consumos = await _servicio.ObtenerPorTrabajoAsync(id_trabajo);
            var dtos = consumos.Select(c => new ConsumoMaterialDto
            {
                id_consumo_material = c.id_consumo_material,
                id_trabajo = c.id_trabajo,
                id_material = c.id_material,
                cantidad = c.cantidad,
                fecha_hora = c.fecha_hora
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsumoMaterialCrearDto dto)
        {
            var entidad = new ConsumoMaterial
            {
                id_trabajo = dto.id_trabajo,
                id_material = dto.id_material,
                cantidad = dto.cantidad,
                fecha_hora = dto.fecha_hora ?? DateTime.Now
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ConsumoMaterialActualizarDto dto)
        {
            var entidad = new ConsumoMaterial
            {
                id_consumo_material = id,
                id_trabajo = dto.id_trabajo,
                id_material = dto.id_material,
                cantidad = dto.cantidad,
                fecha_hora = dto.fecha_hora
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
