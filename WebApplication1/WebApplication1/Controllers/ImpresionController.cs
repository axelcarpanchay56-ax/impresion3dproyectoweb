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
    public class ImpresionController : ControllerBase
    {
        private readonly ServicioModelo3D _modeloServicio;
        private readonly IValidator<Modelo3DCrearDto> _validatorCrear;
        private readonly IMapper _mapper;

        public ImpresionController(
            ServicioModelo3D modeloServicio,
            IValidator<Modelo3DCrearDto> validatorCrear,
            IMapper mapper)
        {
            _modeloServicio = modeloServicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelos = await _modeloServicio.ObtenerTodosAsync();

            var dtos =
                _mapper.Map<List<Modelo3DDto>>(modelos);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Modelo3DCrearDto dto)
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
                    _mapper.Map<Modelo3D>(dto);

                entidad.fecha_creacion = DateTime.Now;

                var id = await _modeloServicio.CrearAsync(entidad);
                return CreatedAtAction(nameof(Get), new { id }, new { id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }
    }
}
