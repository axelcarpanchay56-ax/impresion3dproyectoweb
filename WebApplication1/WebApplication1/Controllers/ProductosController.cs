using Microsoft.AspNetCore.Mvc;
using impresion3d.Api.DTOs;
using impresion3d.Core.Entidades;
using impresion3d.Core.Servicios;

namespace impresion3d.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ServicioProducto _servicio;

        public ProductosController(ServicioProducto servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos =
                await _servicio.ObtenerTodosAsync();

            var productosDto = productos.Select(producto => new ProductoDto
            {
                IdProducto = producto.IdProducto,
                id_modelo3d = producto.id_modelo3d,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio_venta = producto.precio_venta,
                activo = producto.activo,
                fecha_alta = producto.fecha_alta
            }).ToList();

            return Ok(productosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto =
                await _servicio.ObtenerPorIdAsync(id);

            if (producto == null)
            {
                return NotFound(
                    "Producto no encontrado."
                );
            }

            var productoDto = new ProductoDto
            {
                IdProducto = producto.IdProducto,
                id_modelo3d = producto.id_modelo3d,
                nombre = producto.nombre,
                descripcion = producto.descripcion,
                precio_venta = producto.precio_venta,
                activo = producto.activo,
                fecha_alta = producto.fecha_alta
            };

            return Ok(productoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
            CrearProductoDto dto)
        {
            var producto = new Productos
            {
                id_modelo3d = dto.id_modelo3d,
                nombre = dto.nombre,
                descripcion = dto.descripcion,
                precio_venta = dto.precio_venta,
                activo = true,
                fecha_alta = DateTime.Now
            };

            int resultado =
                await _servicio.CrearAsync(producto);

            if (resultado <= 0)
            {
                return BadRequest(
                    "No se pudo crear el producto."
                );
            }

            return Ok(
                "Producto creado correctamente."
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(
            int id,
            ActualizarProductoDto dto)
        {
            var productoExistente =
                await _servicio.ObtenerPorIdAsync(id);

            if (productoExistente == null)
            {
                return NotFound(
                    "Producto no encontrado."
                );
            }

            var producto = new Productos
            {
                IdProducto = id,
                id_modelo3d = dto.id_modelo3d,
                nombre = dto.nombre,
                descripcion = dto.descripcion,
                precio_venta = dto.precio_venta,
                activo = dto.activo,
                fecha_alta = dto.fecha_alta
            };

            bool resultado =
                await _servicio.ActualizarAsync(producto);

            if (!resultado)
            {
                return BadRequest(
                    "No se pudo actualizar el producto."
                );
            }

            return Ok(
                "Producto actualizado correctamente."
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
                    "No se pudo cambiar el estado del producto."
                );
            }

            return Ok(
                "Estado del producto actualizado correctamente."
            );
        }
    }
}