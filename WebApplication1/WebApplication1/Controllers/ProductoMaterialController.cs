using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoMaterialController : ControllerBase
    {
        private readonly ServicioProductoMaterial _servicio;

        public ProductoMaterialController(
            ServicioProductoMaterial servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("producto/{idProducto}")]
        public async Task<ActionResult<List<ProductoMaterialDto>>>
            ObtenerPorProducto(int idProducto)
        {
            var lista =
                await _servicio.ObtenerPorProductoAsync(idProducto);

            var resultado = lista.Select(pm => new ProductoMaterialDto
            {
                IdProducto = pm.IdProducto,
                IdMaterial = pm.IdMaterial,
                cantidad_estimada = pm.cantidad_estimada
            }).ToList();

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult> Asignar(
            AsignarProductoMaterialDto dto)
        {
            var productoMaterial = new ProductoMaterial
            {
                IdProducto = dto.IdProducto,
                IdMaterial = dto.IdMaterial,
                cantidad_estimada = dto.cantidad_estimada
            };

            int resultado =
                await _servicio.AsignarAsync(productoMaterial);

            if (resultado > 0)
                return Ok();

            return BadRequest();
        }

        [HttpPut("{idProducto}/{idMaterial}")]
        public async Task<ActionResult> Actualizar(
            int idProducto,
            int idMaterial,
            ActualizarProductoMaterialDto dto)
        {
            var productoMaterial = new ProductoMaterial
            {
                IdProducto = idProducto,
                IdMaterial = idMaterial,
                cantidad_estimada = dto.cantidad_estimada
            };

            bool resultado =
                await _servicio.ActualizarAsync(productoMaterial);

            if (resultado)
                return Ok();

            return NotFound();
        }

        [HttpDelete("{idProducto}/{idMaterial}")]
        public async Task<ActionResult> Eliminar(
            int idProducto,
            int idMaterial)
        {
            bool resultado =
                await _servicio.EliminarAsync(
                    idProducto,
                    idMaterial
                );

            if (resultado)
                return Ok();

            return NotFound();
        }
    }
} 
