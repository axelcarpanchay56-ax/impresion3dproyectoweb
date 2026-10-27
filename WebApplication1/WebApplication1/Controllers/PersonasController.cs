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

        public PersonasController(ServicioPersona servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personas = await _servicio.AllAsync();

            var personasDto = personas.Select(persona => new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Dni = persona.Dni,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Telefono = persona.Telefono,
                Email = persona.Email,
                Direccion = persona.Direccion,
                FechaAlta = persona.FechaAlta,
                Activo = persona.Activo
            }).ToList();

            return Ok(personasDto); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var persona = await _servicio.GetByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            var personaDto = new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Dni = persona.Dni,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Telefono = persona.Telefono,
                Email = persona.Email,
                Direccion = persona.Direccion,
                FechaAlta = persona.FechaAlta,
                Activo = persona.Activo
            };

            return Ok(personaDto); 
        }

        [HttpGet("dni/{dni}")]
        public async Task<IActionResult> GetByDni(string dni)
        {
            var persona = await _servicio.GetByDniAsync(dni);

            if (persona == null)
            {
                return NotFound();
            }

            if (persona == null)
            {
                return NotFound();
            }

            var personaDto = new PersonaDto
            {
                IdPersona = persona.IdPersona,
                Dni = persona.Dni,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Telefono = persona.Telefono,
                Email = persona.Email,
                Direccion = persona.Direccion,
                FechaAlta = persona.FechaAlta,
                Activo = persona.Activo
            };

            return Ok(personaDto);
            }

            [HttpPost]
            public async Task<IActionResult> Crear(CrearPersonaDto dto)
            {
                var persona = new Persona
                {
                    Dni = dto.Dni,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Telefono = dto.Telefono,
                    Email = dto.Email,
                    Direccion = dto.Direccion,
                    Activo = true,
                    FechaAlta = DateTime.Now
                };

                int resultado = await _servicio.CrearAsync(persona);

                if (resultado <= 0)
                {
                    return BadRequest("No se pudo crear la persona.");
                }

                return Ok("Persona creada correctamente.");
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
                return NotFound("Persona no encontrada.");
            }

            var persona = new Persona
            {
                IdPersona = id,
                Dni = dto.Dni,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Direccion = dto.Direccion,
                Activo = dto.Activo,
                FechaAlta = dto.FechaAlta
            };

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
                return NotFound("Persona no encontrada.");
            }

            bool resultado =
                await _servicio.CambiarEstadoAsync(id, activo);

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
 
