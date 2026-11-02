using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpresorasController : ControllerBase
    {
        private readonly ServicioImpresoras _servicio;

        public ImpresorasController(ServicioImpresoras servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var impresoras = await _servicio.ObtenerTodosAsync();
            var dtos = impresoras.Select(m => new ImpresoraDto
            {
                id_impresora = m.id_impresora,
                nombre = m.nombre,
                marca = m.marca,
                modelo = m.modelo,
                volumen_x = m.volumen_x,
                volumen_y = m.volumen_y,
                volumen_z = m.volumen_z,
                tecnologia = m.tecnologia,
                estado = m.estado,
                fecha_alta = m.fecha_alta
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var m = await _servicio.ObtenerPorIdAsync(id);
            var dto = new ImpresoraDto
            {
                id_impresora = m.id_impresora,
                nombre = m.nombre,
                marca = m.marca,
                modelo = m.modelo,
                volumen_x = m.volumen_x,
                volumen_y = m.volumen_y,
                volumen_z = m.volumen_z,
                tecnologia = m.tecnologia,
                estado = m.estado,
                fecha_alta = m.fecha_alta
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImpresoraCrearDto dto)
        {
            var entidad = new Impresoras
            {
                nombre = dto.nombre,
                marca = dto.marca,
                modelo = dto.modelo,
                volumen_x = dto.volumen_x,
                volumen_y = dto.volumen_y,
                volumen_z = dto.volumen_z,
                tecnologia = dto.tecnologia,
                estado = dto.estado,
                fecha_alta = DateTime.Now
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ImpresoraActualizarDto dto)
        {
            var entidad = new Impresoras
            {
                id_impresora = id,
                nombre = dto.nombre,
                marca = dto.marca,
                modelo = dto.modelo,
                volumen_x = dto.volumen_x,
                volumen_y = dto.volumen_y,
                volumen_z = dto.volumen_z,
                tecnologia = dto.tecnologia,
                estado = dto.estado
            };

            await _servicio.ActualizarAsync(entidad);
            return NoContent();
        }
    }
}
