using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoMaterialController : ControllerBase
    {
        private readonly ServicioMovimientoMaterial _servicio;

        public MovimientoMaterialController(
            ServicioMovimientoMaterial servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovimientoMaterialDto>>> ObtenerTodos()
        {
            var lista = await _servicio.ObtenerTodosAsync();

            var resultado = lista.Select(m => new MovimientoMaterialDto
            {
                IdMovimientoMaterial = m.IdMovimientoMaterial,
                id_material = m.id_material,
                tipo_movimiento = m.tipo_movimiento,
                cantidad = m.cantidad,
                fecha_hora = m.fecha_hora,
                observacion = m.observacion
            }).ToList();

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoMaterialDto>> ObtenerPorId(int id)
        {
            var movimiento = await _servicio.ObtenerPorIdAsync(id);

            if (movimiento == null)
                return NotFound();

            var dto = new MovimientoMaterialDto
            {
                IdMovimientoMaterial = movimiento.IdMovimientoMaterial,
                id_material = movimiento.id_material,
                tipo_movimiento = movimiento.tipo_movimiento,
                cantidad = movimiento.cantidad,
                fecha_hora = movimiento.fecha_hora,
                observacion = movimiento.observacion
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(CrearMovimientoMaterialDto dto)
        {
            var movimiento = new MovimientoMaterial
            {
                id_material = dto.id_material,
                tipo_movimiento = dto.tipo_movimiento,
                cantidad = dto.cantidad,
                fecha_hora = dto.fecha_hora,
                observacion = dto.observacion
            };

            int id = await _servicio.CrearAsync(movimiento);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(
            int id,
            ActualizarMovimientoMaterialDto dto)
        {
            var movimiento = new MovimientoMaterial
            {
                IdMovimientoMaterial = id,
                id_material = dto.id_material,
                tipo_movimiento = dto.tipo_movimiento,
                cantidad = dto.cantidad,
                fecha_hora = dto.fecha_hora,
                observacion = dto.observacion
            };

            bool resultado = await _servicio.ActualizarAsync(movimiento);

            if (resultado)
                return Ok();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            bool resultado = await _servicio.EliminarAsync(id);

            if (resultado)
                return Ok();

            return NotFound();
        }
    }
} 
