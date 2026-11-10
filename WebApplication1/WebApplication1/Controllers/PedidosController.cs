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
    public class PedidosController : ControllerBase
    {
        private readonly ServicioPedidos _servicio;
        private readonly IValidator<PedidosCrearDto> _validatorCrear;
        private readonly IMapper _mapper;

        public PedidosController(
            ServicioPedidos servicio,
            IValidator<PedidosCrearDto> validatorCrear,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrear = validatorCrear;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _servicio.ObtenerTodosAsync();

            var dtos =
                _mapper.Map<List<PedidosDto>>(pedidos);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var p = await _servicio.ObtenerPorIdAsync(id);

                var dto =
                    _mapper.Map<PedidosDto>(p);

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

        [HttpGet("cliente/{id_cliente}")]
        public async Task<IActionResult> GetByCliente(int id_cliente)
        {
            var pedidos = await _servicio.ObtenerPorClienteAsync(id_cliente);

            var dtos =
                _mapper.Map<List<PedidosDto>>(pedidos);

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidosCrearDto dto)
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
                    _mapper.Map<Pedidos>(dto);

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
        public async Task<IActionResult> Put(int id, [FromBody] PedidosActualizarDto dto)
        {
            try
            {
                var entidad =
                    _mapper.Map<Pedidos>(dto);

                entidad.id_pedido = id;

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
