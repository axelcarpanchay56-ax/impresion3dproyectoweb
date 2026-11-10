using AutoMapper;
using FluentValidation;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientoMaterialController : ControllerBase
    {
        private readonly ServicioMovimientoMaterial _servicio;
        private readonly IValidator<CrearMovimientoMaterialDto> _validatorCrear;
        private readonly IMapper _mapper;

        public MovimientoMaterialController(
            ServicioMovimientoMaterial servicio,
            IValidator<CrearMovimientoMaterialDto> validatorCrear,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovimientoMaterialDto>>> ObtenerTodos()
        {
            var lista = await _servicio.ObtenerTodosAsync();

            var resultado =
                _mapper.Map<List<MovimientoMaterialDto>>(lista);

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoMaterialDto>> ObtenerPorId(int id)
        {
            var movimiento = await _servicio.ObtenerPorIdAsync(id);

            if (movimiento == null)
                return NotFound();

            var dto =
                _mapper.Map<MovimientoMaterialDto>(movimiento);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(CrearMovimientoMaterialDto dto)
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

            var movimiento =
                _mapper.Map<MovimientoMaterial>(dto);

            if (movimiento.fecha_hora == default)
            {
                movimiento.fecha_hora = DateTime.Now;
            }

            int id = await _servicio.CrearAsync(movimiento);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(
            int id,
            ActualizarMovimientoMaterialDto dto)
        {
            var movimiento =
                _mapper.Map<MovimientoMaterial>(dto);

            movimiento.IdMovimientoMaterial = id;

            bool resultado = await _servicio.ActualizarAsync(movimiento);

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
