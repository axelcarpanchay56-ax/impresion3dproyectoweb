using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpresoraMaterialController : ControllerBase
    {
        private readonly ServicioImpresoraMaterial _servicio;

        public ImpresoraMaterialController(ServicioImpresoraMaterial servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _servicio.ObtenerTodosAsync();
            var dtos = lista.Select(x => new ImpresoraMaterialDto
            {
                id_impresora = x.id_impresora,
                id_material = x.id_material,
                observacion = x.observacion
            });
            return Ok(dtos);
        }

        [HttpGet("{id_impresora}/{id_material}")]
        public async Task<IActionResult> GetById(int id_impresora, int id_material)
        {
            var item = await _servicio.ObtenerPorIdAsync(id_impresora, id_material);
            var dto = new ImpresoraMaterialDto
            {
                id_impresora = item.id_impresora,
                id_material = item.id_material,
                observacion = item.observacion
            };
            return Ok(dto);
        }

        [HttpGet("impresora/{id_impresora}")]
        public async Task<IActionResult> GetByImpresora(int id_impresora)
        {
            var lista = await _servicio.ObtenerPorImpresoraAsync(id_impresora);
            var dtos = lista.Select(x => new ImpresoraMaterialDto
            {
                id_impresora = x.id_impresora,
                id_material = x.id_material,
                observacion = x.observacion
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImpresoraMaterialCrearDto dto)
        {
            var entidad = new ImpresoraMaterial
            {
                id_impresora = dto.id_impresora,
                id_material = dto.id_material,
                observacion = dto.observacion
            };

            await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id_impresora = entidad.id_impresora, id_material = entidad.id_material }, dto);
        }

        [HttpPut("{id_impresora}/{id_material}")]
        public async Task<IActionResult> Put(int id_impresora, int id_material, [FromBody] ImpresoraMaterialActualizarDto dto)
        {
            var entidad = new ImpresoraMaterial
            {
                id_impresora = id_impresora,
                id_material = id_material,
                observacion = dto.observacion
            };

            await _servicio.ActualizarAsync(entidad);
            return NoContent();
        }

        [HttpDelete("{id_impresora}/{id_material}")]
        public async Task<IActionResult> Delete(int id_impresora, int id_material)
        {
            await _servicio.EliminarAsync(id_impresora, id_material);
            return NoContent();
        }
    }
}
