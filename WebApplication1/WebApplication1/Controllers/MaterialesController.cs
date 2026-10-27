using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly ServicioMaterial _servicio;

        public MaterialesController(ServicioMaterial servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materiales =
                await _servicio.ObtenerTodosAsync();

            var materialesDto = materiales.Select(material => new MaterialDto
            {
                IdMaterial = material.IdMaterial,
                nombre = material.nombre,
                tipo = material.tipo,
                unidad_medida = material.unidad_medida,
                color = material.color,
                diametro = material.diametro,
                activo = material.activo
            }).ToList();

            return Ok(materialesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var material =
                await _servicio.ObtenerPorIdAsync(id);

            if (material == null)
            {
                return NotFound(
                    "Material no encontrado."
                );
            }

            var materialDto = new MaterialDto
            {
                IdMaterial = material.IdMaterial,
                nombre = material.nombre,
                tipo = material.tipo,
                unidad_medida = material.unidad_medida,
                color = material.color,
                diametro = material.diametro,
                activo = material.activo
            };

            return Ok(materialDto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
            CrearMaterialDto dto)
        {
            var material = new Materiales
            {
                nombre = dto.nombre,
                tipo = dto.tipo,
                unidad_medida = dto.unidad_medida,
                color = dto.color,
                diametro = dto.diametro,
                activo = true
            };

            int resultado =
                await _servicio.CrearAsync(material);

            if (resultado <= 0)
            {
                return BadRequest(
                    "No se pudo crear el material."
                );
            }

            return Ok(
                "Material creado correctamente."
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(
            int id,
            ActualizarMaterialDto dto)
        {
            var materialExistente =
                await _servicio.ObtenerPorIdAsync(id);

            if (materialExistente == null)
            {
                return NotFound(
                    "Material no encontrado."
                );
            }

            var material = new Materiales
            {
                IdMaterial = id,
                nombre = dto.nombre,
                tipo = dto.tipo,
                unidad_medida = dto.unidad_medida,
                color = dto.color,
                diametro = dto.diametro,
                activo = dto.activo
            };

            bool resultado =
                await _servicio.ActualizarAsync(material);

            if (!resultado)
            {
                return BadRequest(
                    "No se pudo actualizar el material."
                );
            }

            return Ok(
                "Material actualizado correctamente."
            );
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(
            int id,
            bool activo)
        {
            bool resultado =
                await _servicio.CambiarEstadoAsync(
                    id,
                    activo
                );

            if (!resultado)
            {
                return BadRequest(
                    "No se pudo cambiar el estado del material."
                );
            }

            return Ok(
                "Estado del material actualizado correctamente."
            );
        }
    }
} 