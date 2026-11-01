using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrabajoImpresionController : ControllerBase
    {
        private readonly ServicioTrabajoImpresion _servicio;

        public TrabajoImpresionController(ServicioTrabajoImpresion servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trabajos = await _servicio.ObtenerTodosAsync();
            var dtos = trabajos.Select(t => new TrabajoImpresionDto
            {
                id_trabajo_impresion = t.id_trabajo_impresion,
                id_pedido = t.id_pedido,
                id_producto = t.id_producto,
                id_impresora = t.id_impresora,
                id_modelo = t.id_modelo,
                id_material = t.id_material,
                cantidad = t.cantidad,
                id_estado = t.id_estado,
                fecha_inicio = t.fecha_inicio,
                fecha_fin = t.fecha_fin,
                tiempo_estimado = t.tiempo_estimado,
                peso_estimado = t.peso_estimado,
                observacion = t.observacion
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trabajo = await _servicio.ObtenerPorIdAsync(id);
            var dto = new TrabajoImpresionDto
            {
                id_trabajo_impresion = trabajo.id_trabajo_impresion,
                id_pedido = trabajo.id_pedido,
                id_producto = trabajo.id_producto,
                id_impresora = trabajo.id_impresora,
                id_modelo = trabajo.id_modelo,
                id_material = trabajo.id_material,
                cantidad = trabajo.cantidad,
                id_estado = trabajo.id_estado,
                fecha_inicio = trabajo.fecha_inicio,
                fecha_fin = trabajo.fecha_fin,
                tiempo_estimado = trabajo.tiempo_estimado,
                peso_estimado = trabajo.peso_estimado,
                observacion = trabajo.observacion
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TrabajoImpresionCrearDto dto)
        {
            var entidad = new TrabajoImpresion
            {
                id_pedido = dto.id_pedido,
                id_producto = dto.id_producto,
                id_impresora = dto.id_impresora,
                id_modelo = dto.id_modelo,
                id_material = dto.id_material,
                cantidad = dto.cantidad,
                id_estado = dto.id_estado,
                fecha_inicio = dto.fecha_inicio == default ? DateTime.Now : dto.fecha_inicio,
                peso_estimado = dto.peso_estimado,
                observacion = dto.observacion
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TrabajoImpresionActualizarDto dto)
        {
            var entidad = new TrabajoImpresion
            {
                id_trabajo_impresion = id,
                id_pedido = dto.id_pedido,
                id_producto = dto.id_producto,
                id_impresora = dto.id_impresora,
                id_modelo = dto.id_modelo,
                id_material = dto.id_material,
                cantidad = dto.cantidad,
                id_estado = dto.id_estado,
                fecha_inicio = dto.fecha_inicio,
                fecha_fin = dto.fecha_fin,
                tiempo_estimado = dto.tiempo_estimado,
                peso_estimado = dto.peso_estimado,
                observacion = dto.observacion
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
