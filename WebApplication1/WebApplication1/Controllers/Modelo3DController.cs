using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<Modelo3DDto>>> ObtenerTodos()
        {
            var lista = await _servicio.ObtenerTodosAsync();

            var resultado = lista.Select(m => new Modelo3DDto
            {
                IdModelo3d = m.IdModelo3d,
                nombre = m.nombre,
                archivo = m.archivo,
                formato = m.formato,
                descripcion = m.descripcion,
                fecha_creacion = m.fecha_creacion
            }).ToList();

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo3DDto>> ObtenerPorId(int id)
        {
            var modelo = await _servicio.ObtenerPorIdAsync(id);

            if (modelo == null)
                return NotFound();

            var dto = new Modelo3DDto
            {
                IdModelo3d = modelo.IdModelo3d,
                nombre = modelo.nombre,
                archivo = modelo.archivo,
                formato = modelo.formato,
                descripcion = modelo.descripcion,
                fecha_creacion = modelo.fecha_creacion
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(CrearModelo3DDto dto)
        {
            var modelo = new Modelo3D
            {
                nombre = dto.nombre,
                archivo = dto.archivo,
                formato = dto.formato,
                descripcion = dto.descripcion,
                fecha_creacion = dto.fecha_creacion
            };

            int id = await _servicio.CrearAsync(modelo);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(
            int id,
            ActualizarModelo3DDto dto)
        {
            var modelo = new Modelo3D
            {
                IdModelo3d = id,
                nombre = dto.nombre,
                archivo = dto.archivo,
                formato = dto.formato,
                descripcion = dto.descripcion,
                fecha_creacion = dto.fecha_creacion
            };

            bool resultado = await _servicio.ActualizarAsync(modelo);

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
