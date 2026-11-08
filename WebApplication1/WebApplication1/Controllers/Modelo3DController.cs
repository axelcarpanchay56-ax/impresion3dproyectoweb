using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Modelo3DController : ControllerBase
    {
        private readonly ServicioModelo3D _servicio;

        public Modelo3DController(ServicioModelo3D servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelos = await _servicio.ObtenerTodosAsync();
            var dtos = modelos.Select(m => new Modelo3DDto
            {
                id_modelo3d = m.id_modelo3d,
                nombre = m.nombre,
                archivo = m.archivo,
                formato = m.formato,
                descripcion = m.descripcion,
                fecha_creacion = m.fecha_creacion
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var m = await _servicio.ObtenerPorIdAsync(id);
            var dto = new Modelo3DDto
            {
                id_modelo3d = m.id_modelo3d,
                nombre = m.nombre,
                archivo = m.archivo,
                formato = m.formato,
                descripcion = m.descripcion,
                fecha_creacion = m.fecha_creacion
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Modelo3DCrearDto dto)
        {
            var entidad = new Modelo3D
            {
                nombre = dto.nombre,
                archivo = dto.archivo,
                formato = dto.formato,
                descripcion = dto.descripcion,
                fecha_creacion = DateTime.Now
            };

            var id = await _servicio.CrearAsync(entidad);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Modelo3DActualizacionDto dto)
        {
            var entidad = new Modelo3D
            {
                id_modelo3d = id,
                nombre = dto.nombre,
                archivo = dto.archivo,
                formato = dto.formato,
                descripcion = dto.descripcion
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
