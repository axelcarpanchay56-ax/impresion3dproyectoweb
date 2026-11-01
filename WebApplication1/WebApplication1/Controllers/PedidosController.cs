using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ServicioPedidos _servicio;

        public PedidosController(ServicioPedidos servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _servicio.ObtenerTodosAsync();
            var dtos = pedidos.Select(p => new PedidosDto
            {
                id_pedido = p.id_pedido,
                id_cliente = p.id_cliente,
                fecha_hora = p.fecha_hora,
                fecha_entrega_estimada = p.fecha_entrega_estimada,
                fecha_entrega_real = p.fecha_entrega_real,
                id_estado = p.id_estado,
                total_estimado = p.total_estimado,
                subtotal = p.subtotal,
                descuento = p.descuento,
                total = p.total
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _servicio.ObtenerPorIdAsync(id);
            var dto = new PedidosDto
            {
                id_pedido = p.id_pedido,
                id_cliente = p.id_cliente,
                fecha_hora = p.fecha_hora,
                fecha_entrega_estimada = p.fecha_entrega_estimada,
                fecha_entrega_real = p.fecha_entrega_real,
                id_estado = p.id_estado,
                total_estimado = p.total_estimado,
                subtotal = p.subtotal,
                descuento = p.descuento,
                total = p.total
            };
            return Ok(dto);
        }

        [HttpGet("cliente/{id_cliente}")]
        public async Task<IActionResult> GetByCliente(int id_cliente)
        {
            var pedidos = await _servicio.ObtenerPorClienteAsync(id_cliente);
            var dtos = pedidos.Select(p => new PedidosDto
            {
                id_pedido = p.id_pedido,
                id_cliente = p.id_cliente,
                fecha_hora = p.fecha_hora,
                fecha_entrega_estimada = p.fecha_entrega_estimada,
                fecha_entrega_real = p.fecha_entrega_real,
                id_estado = p.id_estado,
                total_estimado = p.total_estimado,
                subtotal = p.subtotal,
                descuento = p.descuento,
                total = p.total
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidosCrearDto dto)
        {
            var entidad = new Pedidos
            {
                id_cliente = dto.id_cliente,
                fecha_hora = dto.fecha_hora ?? DateTime.Now,
                fecha_entrega_estimada = dto.fecha_entrega_estimada,
                fecha_entrega_real = dto.fecha_entrega_real,
                id_estado = dto.id_estado,
                total_estimado = dto.total_estimado,
                subtotal = dto.subtotal,
                descuento = dto.descuento,
                total = dto.total
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PedidosActualizarDto dto)
        {
            var entidad = new Pedidos
            {
                id_pedido = id,
                id_cliente = dto.id_cliente,
                fecha_hora = dto.fecha_hora,
                fecha_entrega_estimada = dto.fecha_entrega_estimada,
                fecha_entrega_real = dto.fecha_entrega_real,
                id_estado = dto.id_estado,
                total_estimado = dto.total_estimado,
                subtotal = dto.subtotal,
                descuento = dto.descuento,
                total = dto.total
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
