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
    public class ImpresoraMaterialController : ControllerBase
    {
        private readonly ServicioImpresoraMaterial _servicio;
        private readonly IValidator<ImpresoraMaterialCrearDto> _validatorCrear;
        private readonly IMapper _mapper;

        public ImpresoraMaterialController(
            ServicioImpresoraMaterial servicio,
            IValidator<ImpresoraMaterialCrearDto> validatorCrear,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _servicio.ObtenerTodosAsync();

            var dtos =
                _mapper.Map<List<ImpresoraMaterialDto>>(lista);

            return Ok(dtos);
        }

        [HttpGet("{id_impresora}/{id_material}")]
        public async Task<IActionResult> GetById(int id_impresora, int id_material)
        {
            try
            {
                var item = await _servicio.ObtenerPorIdAsync(id_impresora, id_material);

                var dto =
                    _mapper.Map<ImpresoraMaterialDto>(item);

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

        [HttpGet("impresora/{id_impresora}")]
        public async Task<IActionResult> GetByImpresora(int id_impresora)
        {
            var lista = await _servicio.ObtenerPorImpresoraAsync(id_impresora);

            var dtos =
                _mapper.Map<List<ImpresoraMaterialDto>>(lista);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImpresoraMaterialCrearDto dto)
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
                    _mapper.Map<ImpresoraMaterial>(dto);

                await _servicio.CrearAsync(entidad);
                return CreatedAtAction(
                    nameof(GetById),
                    new
                    {
                        id_impresora = entidad.id_impresora,
                        id_material = entidad.id_material
                    },
                    dto
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPut("{id_impresora}/{id_material}")]
        public async Task<IActionResult> Put(
            int id_impresora,
            int id_material,
            [FromBody] ImpresoraMaterialActualizarDto dto)
        {
            try
            {
                var entidad =
                    _mapper.Map<ImpresoraMaterial>(dto);

                entidad.id_impresora = id_impresora;
                entidad.id_material = id_material;

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

        [HttpDelete("{id_impresora}/{id_material}")]
        public async Task<IActionResult> Delete(int id_impresora, int id_material)
        {
            try
            {
                await _servicio.EliminarAsync(id_impresora, id_material);
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
