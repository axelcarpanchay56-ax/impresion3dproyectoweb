using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoPedidoController : ControllerBase
    {
        private readonly ServicioEstadoPedido _servicio;

        public EstadoPedidoController(ServicioEstadoPedido servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estados = await _servicio.ObtenerTodosAsync();
            var dtos = estados.Select(e => new EstadoPedidoDto
            {
                id_estado_pedido = e.id_estado_pedido,
                nombre = e.nombre
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _servicio.ObtenerPorIdAsync(id);
            var dto = new EstadoPedidoDto
            {
                id_estado_pedido = e.id_estado_pedido,
                nombre = e.nombre
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstadoPedidoCrearDto dto)
        {
            var entidad = new EstadoPedido
            {
                nombre = dto.nombre
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstadoPedidoActualizarDto dto)
        {
            var entidad = new EstadoPedido
            {
                id_estado_pedido = id,
                nombre = dto.nombre
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
