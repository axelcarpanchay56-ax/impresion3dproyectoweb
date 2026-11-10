using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumoMaterialController : ControllerBase
    {
        private readonly ServicioConsumoMaterial _servicio;
        private readonly IValidator<ConsumoMaterialCrearDto> _validatorCrear;
        private readonly IMapper _mapper;

        public ConsumoMaterialController(
            ServicioConsumoMaterial servicio,
            IValidator<ConsumoMaterialCrearDto> validatorCrear,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var consumos = await _servicio.ObtenerTodosAsync();

            var dtos =
                _mapper.Map<List<ConsumoMaterialDto>>(consumos);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var c = await _servicio.ObtenerPorIdAsync(id);

                var dto =
                    _mapper.Map<ConsumoMaterialDto>(c);

                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpGet("trabajo/{id_trabajo}")]
        public async Task<IActionResult> GetByTrabajo(int id_trabajo)
        {
            var consumos = await _servicio.ObtenerPorTrabajoAsync(id_trabajo);

            var dtos =
                _mapper.Map<List<ConsumoMaterialDto>>(consumos);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsumoMaterialCrearDto dto)
        {
            var validationResultado =
                await _validatorCrear.ValidateAsync(dto);

            if (!validationResultado.IsValid)
            {
                var errores = validationResultado.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Mensaje = "Error de validación",
                    Errores = errores
                });
            }

            try
            {
                var entidad =
                    _mapper.Map<ConsumoMaterial>(dto);

                if (entidad.fecha_hora == default)
                {
                    entidad.fecha_hora = DateTime.Now;
                }

                var id = await _servicio.CrearAsync(entidad);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ConsumoMaterialActualizarDto dto)
        {
            try
            {
                var entidad =
                    _mapper.Map<ConsumoMaterial>(dto);

                entidad.id_consumo_material = id;

                await _servicio.ActualizarAsync(entidad);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    Mensaje = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servicio.EliminarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    Mensaje = ex.Message
                });
            }
        }
    }
}
