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
    public class UsuarioRolController : ControllerBase
    {
        private readonly ServicioUsuarioRol _servicio;
        private readonly IValidator<AsignarUsuarioRolDto> _validatorAsignar;
        private readonly IMapper _mapper;

        public UsuarioRolController(
            ServicioUsuarioRol servicio,
            IValidator<AsignarUsuarioRolDto> validatorAsignar,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorAsignar = validatorAsignar;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(
            AsignarUsuarioRolDto dto)
        {
            var validationResultado =
                await _validatorAsignar.ValidateAsync(dto);

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
                var usuarioRol =
                    _mapper.Map<Usuario_Rol>(dto);

                int resultado =
                    await _servicio.AsignarAsync(usuarioRol);

                if (resultado <= 0)
                {
                    return BadRequest(
                        "No se pudo asignar el rol al usuario."
                    );
                }

                return Ok(
                    "Rol asignado correctamente."
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

        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> ObtenerPorUsuario(
            int idUsuario)
        {
            try
            {
                var roles =
                    await _servicio.ObtenerPorUsuarioAsync(idUsuario);

                var rolesDto =
                    _mapper.Map<List<UsuarioRolDto>>(roles);

                return Ok(rolesDto);
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
