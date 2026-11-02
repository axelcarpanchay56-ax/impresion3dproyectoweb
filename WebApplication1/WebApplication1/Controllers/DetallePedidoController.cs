using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        private readonly ServicioDetallePedido _servicio;

        public DetallePedidoController(ServicioDetallePedido servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var detalles = await _servicio.ObtenerTodosAsync();
            var dtos = detalles.Select(d => new DetallePedidoDto
            {
                id_detalle_pedido = d.id_detalle_pedido,
                id_pedido = d.id_pedido,
                id_producto = d.id_producto,
                cantidad = d.cantidad,
                precio_unitario = d.precio_unitario,
                subtotal = d.subtotal
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var d = await _servicio.ObtenerPorIdAsync(id);
            var dto = new DetallePedidoDto
            {
                id_detalle_pedido = d.id_detalle_pedido,
                id_pedido = d.id_pedido,
                id_producto = d.id_producto,
                cantidad = d.cantidad,
                precio_unitario = d.precio_unitario,
                subtotal = d.subtotal
            };
            return Ok(dto);
        }

        [HttpGet("pedido/{id_pedido}")]
        public async Task<IActionResult> GetByPedido(int id_pedido)
        {
            var detalles = await _servicio.ObtenerPorPedidoAsync(id_pedido);
            var dtos = detalles.Select(d => new DetallePedidoDto
            {
                id_detalle_pedido = d.id_detalle_pedido,
                id_pedido = d.id_pedido,
                id_producto = d.id_producto,
                cantidad = d.cantidad,
                precio_unitario = d.precio_unitario,
                subtotal = d.subtotal
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DetallePedidoCrearDto dto)
        {
            var entidad = new DetallePedido
            {
                id_pedido = dto.id_pedido,
                id_producto = dto.id_producto,
                cantidad = dto.cantidad,
                precio_unitario = dto.precio_unitario,
                subtotal = dto.subtotal
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DetallePedidoActualizarDto dto)
        {
            var entidad = new DetallePedido
            {
                id_detalle_pedido = id,
                id_pedido = dto.id_pedido,
                id_producto = dto.id_producto,
                cantidad = dto.cantidad,
                precio_unitario = dto.precio_unitario,
                subtotal = dto.subtotal
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
