using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class ProductoCrearValidator : AbstractValidator<CrearProductoDto>
    {
        public ProductoCrearValidator()
        {
            RuleFor(x => x.nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del producto no puede superar los 100 caracteres.");

            RuleFor(x => x.precio_venta)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El precio de venta no puede ser negativo.");
        }
    }
}
