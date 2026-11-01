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

        public ClientesController(ServicioCliente servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _servicio.ObtenerTodosAsync();

            var clientesDto = clientes.Select(cliente => new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                IdPersona = cliente.id_persona
            }).ToList();

            return Ok(clientesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearClienteDto dto)
        {
            var cliente = new Clientes
            {
                id_persona = dto.IdPersona
            };

            int resultado = await _servicio.CrearAsync(cliente);

            if (resultado <= 0)
            {
                return BadRequest("No se pudo crear el cliente.");
            }

            return Ok("Cliente creado correctamente.");
        } 
    }
}

