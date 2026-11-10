using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class ProductoMaterialAsignarValidator : AbstractValidator<AsignarProductoMaterialDto>
    {
        public ProductoMaterialAsignarValidator()
        {
            RuleFor(x => x.IdProducto)
                .GreaterThan(0)
                .WithMessage("El ID del producto debe ser mayor a 0.");

            RuleFor(x => x.IdMaterial)
                .GreaterThan(0)
                .WithMessage("El ID del material debe ser mayor a 0.");

            RuleFor(x => x.cantidad_estimada)
                .GreaterThan(0)
                .WithMessage("La cantidad estimada debe ser mayor a 0.");
        }
    }
}
