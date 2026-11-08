using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoImpresionController : ControllerBase
    {
        private readonly ServicioEstadoImpresion _servicio;

        public EstadoImpresionController(ServicioEstadoImpresion servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estados = await _servicio.ObtenerTodosAsync();
            var dtos = estados.Select(e => new EstadoImpresionDto
            {
                id_estado_impresion = e.id_estado_impresion,
                nombre = e.nombre
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _servicio.ObtenerPorIdAsync(id);
            var dto = new EstadoImpresionDto
            {
                id_estado_impresion = e.id_estado_impresion,
                nombre = e.nombre
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstadoImpresionCrearDto dto)
        {
            var entidad = new EstadoImpresion
            {
                nombre = dto.nombre
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstadoImpresionActualizarDto dto)
        {
            var entidad = new EstadoImpresion
            {
                id_estado_impresion = id,
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
