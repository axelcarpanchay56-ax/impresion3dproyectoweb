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
    public class PersonasController : ControllerBase
    {
        private readonly ServicioPersona _servicio;
        private readonly IValidator<CrearPersonaDto> _validatorCrearPersona;
        private readonly IMapper _mapper;

        public PersonasController(
            ServicioPersona servicio,
            IValidator<CrearPersonaDto> validatorCrearPersona,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrearPersona = validatorCrearPersona;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _servicio.AllAsync();

            var personasDto =
                _mapper.Map<List<PersonaDto>>(personas);

            return Ok(personasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona =
                await _servicio.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            var personaDto =
                _mapper.Map<PersonaDto>(persona);

            return Ok(personaDto);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(
            [FromQuery] string termino)
        {
            try
            {
                var personas =
                    await _servicio.BuscarAsync(termino);

                var personasDto =
                    _mapper.Map<List<PersonaDto>>(personas);

                return Ok(personasDto);
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
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                bool resultado =
                    await _servicio.EliminarAsync(id);

                if (!resultado)
                {
                    return NotFound(
                        "Persona no encontrada."
                    );
                }

                return Ok(
                    "Persona eliminada lógicamente correctamente."
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

        [HttpGet("dni/{dni}")]
        public async Task<IActionResult> GetByDni(string dni)
        {
            var persona =
                await _servicio.GetByDniAsync(dni);

            if (persona == null)
            {
                return NotFound();
            }

            var personaDto =
                _mapper.Map<PersonaDto>(persona);

            return Ok(personaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
            CrearPersonaDto dto)
        {
            var validationResultado =
                await _validatorCrearPersona.ValidateAsync(dto);

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
                var persona =
                    _mapper.Map<Persona>(dto);

                persona.Activo = true;
                persona.FechaAlta = DateTime.Now;

                int resultado =
                    await _servicio.CrearAsync(persona);

                if (resultado <= 0)
                {
                    return BadRequest(
                        "No se pudo crear la persona."
                    );
                }

                return Ok(
                    "Persona creada correctamente."
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(
            int id,
            ActualizarPersonaDto dto)
        {
            var personaExistente =
                await _servicio.GetByIdAsync(id);

            if (personaExistente == null)
            {
                return NotFound(
                    "Persona no encontrada."
                );
            }

            var persona =
                _mapper.Map<Persona>(dto);

            persona.IdPersona = id;

            bool resultado =
                await _servicio.ActualizarAsync(persona);

            if (!resultado)
            {
                return BadRequest(
                    "No se pudo actualizar la persona."
                );
            }

            return Ok(
                "Persona actualizada correctamente."
            );
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(
            int id,
            bool activo)
        {
            var personaExistente =
                await _servicio.GetByIdAsync(id);

            if (personaExistente == null)
            {
                return NotFound(
                    "Persona no encontrada."
                );
            }

            bool resultado =
                await _servicio.CambiarEstadoAsync(
                    id,
                    activo
                );

            if (!resultado)
            {
                return BadRequest(
                    "No se pudo cambiar el estado de la persona."
                );
            }

            return Ok(
                "Estado de la persona actualizado correctamente."
            );
        }
    }
} 