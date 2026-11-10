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
    public class ProductoMaterialController : ControllerBase
    {
        private readonly ServicioProductoMaterial _servicio;
        private readonly IValidator<AsignarProductoMaterialDto> _validatorAsignar;
        private readonly IMapper _mapper;

        public ProductoMaterialController(
            ServicioProductoMaterial servicio,
            IValidator<AsignarProductoMaterialDto> validatorAsignar,
            IMapper mapper)
        {
            _servicio = servicio;
            _validatorAsignar = validatorAsignar;
            _mapper = mapper;
        }

        [HttpGet("producto/{idProducto}")]
        public async Task<ActionResult<List<ProductoMaterialDto>>>
            ObtenerPorProducto(int idProducto)
        {
            var lista =
                await _servicio.ObtenerPorProductoAsync(idProducto);

            var resultado =
                _mapper.Map<List<ProductoMaterialDto>>(lista);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult> Asignar(
            AsignarProductoMaterialDto dto)
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

            var productoMaterial =
                _mapper.Map<ProductoMaterial>(dto);

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
            var productoMaterial =
                _mapper.Map<ProductoMaterial>(dto);

            productoMaterial.IdProducto = idProducto;
            productoMaterial.IdMaterial = idMaterial;

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
