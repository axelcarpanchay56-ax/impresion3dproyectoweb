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
    public class ProductosController : ControllerBase
    {
        private readonly ServicioProducto _servicio;
        private readonly IValidator<CrearProductoDto> _validatorCrearProducto;
        private readonly IMapper _mapper;

        public ProductosController(
            ServicioProducto servicio,
            IValidator<CrearProductoDto> validatorCrearProducto,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorCrearProducto = validatorCrearProducto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos =
                await _servicio.ObtenerTodosAsync();

            var productosDto =
                _mapper.Map<List<ProductoDto>>(productos);

            return Ok(productosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var producto =
                    await _servicio.ObtenerPorIdAsync(id);

                if (producto == null)
                {
                    return NotFound(
                        "Producto no encontrado."
                    );
                }

                var productoDto =
                    _mapper.Map<ProductoDto>(producto);

                return Ok(productoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear(
            CrearProductoDto dto)
        {
            var validationResultado =
                await _validatorCrearProducto.ValidateAsync(dto);

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
                var producto =
                    _mapper.Map<Productos>(dto);

                producto.activo = true;
                producto.fecha_alta = DateTime.Now;

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
            ActualizarProductoDto dto)
        {
            try
            {
                var productoExistente =
                    await _servicio.ObtenerPorIdAsync(id);

                if (productoExistente == null)
                {
                    return NotFound(
                        "Producto no encontrado."
                    );
                }

                var producto =
                    _mapper.Map<Productos>(dto);

                producto.IdProducto = id;

                if (producto.fecha_alta == default)
                {
                    producto.fecha_alta = productoExistente.fecha_alta;
                }

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
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Mensaje = ex.Message
                });
            }
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(
            int id,
            bool activo)
        {
            try
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
