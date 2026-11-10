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
    public class ClientesController : ControllerBase
    {
        private readonly ServicioCliente _servicio;
        private readonly IValidator<CrearClienteDto> _validatorCrearCliente;
        private readonly IMapper _mapper;

        public ClientesController(
            ServicioCliente servicio,
            IValidator<CrearClienteDto> validatorCrearCliente,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrearCliente = validatorCrearCliente;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _servicio.ObtenerTodosAsync();

            var clientesDto =
                _mapper.Map<List<ClienteDto>>(clientes);

            return Ok(clientesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearClienteDto dto)
        {
            var validationResultado =
                await _validatorCrearCliente.ValidateAsync(dto);

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
                var cliente =
                    _mapper.Map<Clientes>(dto);

                int resultado =
                    await _servicio.CrearAsync(cliente);

                if (resultado <= 0)
                {
                    return BadRequest(
                        "No se pudo crear el cliente."
                    );
                }

                return Ok(
                    "Cliente creado correctamente."
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
    }
}
