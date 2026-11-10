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
    public class EstadoImpresionController : ControllerBase
    {
        private readonly ServicioEstadoImpresion _servicio;
        private readonly IValidator<EstadoImpresionCrearDto> _validatorCrear;
        private readonly IMapper _mapper;

        public EstadoImpresionController(
            ServicioEstadoImpresion servicio,
            IValidator<EstadoImpresionCrearDto> validatorCrear,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estados = await _servicio.ObtenerTodosAsync();

            var dtos =
                _mapper.Map<List<EstadoImpresionDto>>(estados);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var e = await _servicio.ObtenerPorIdAsync(id);

                var dto =
                    _mapper.Map<EstadoImpresionDto>(e);

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EstadoImpresionCrearDto dto)
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
                    _mapper.Map<EstadoImpresion>(dto);

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
        public async Task<IActionResult> Put(int id, [FromBody] EstadoImpresionActualizarDto dto)
        {
            try
            {
                var entidad =
                    _mapper.Map<EstadoImpresion>(dto);

                entidad.id_estado_impresion = id;

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
