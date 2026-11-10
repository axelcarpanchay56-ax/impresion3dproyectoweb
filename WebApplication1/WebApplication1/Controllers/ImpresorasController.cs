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
    public class ImpresorasController : ControllerBase
    {
        private readonly ServicioImpresoras _servicio;
        private readonly IValidator<ImpresoraCrearDto> _validatorCrear;
        private readonly IMapper _mapper;

        public ImpresorasController(
            ServicioImpresoras servicio,
            IValidator<ImpresoraCrearDto> validatorCrear,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var impresoras = await _servicio.ObtenerTodosAsync();

            var dtos =
                _mapper.Map<List<ImpresoraDto>>(impresoras);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var m = await _servicio.ObtenerPorIdAsync(id);

                var dto =
                    _mapper.Map<ImpresoraDto>(m);

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
        public async Task<IActionResult> Post([FromBody] ImpresoraCrearDto dto)
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
                    _mapper.Map<Impresoras>(dto);

                if (entidad.fecha_alta == default)
                {
                    entidad.fecha_alta = DateTime.Now;
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
        public async Task<IActionResult> Put(int id, [FromBody] ImpresoraActualizarDto dto)
        {
            try
            {
                var entidad =
                    _mapper.Map<Impresoras>(dto);

                entidad.id_impresora = id;

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

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            try
            {
                await _servicio.CambiarEstadoAsync(id, estado);
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
