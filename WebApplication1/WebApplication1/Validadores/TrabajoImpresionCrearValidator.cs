using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class TrabajoImpresionCrearValidator : AbstractValidator<TrabajoImpresionCrearDto>
    {
        public TrabajoImpresionCrearValidator()
        {
            RuleFor(x => x.id_pedido)
                .GreaterThan(0)
                .WithMessage("El ID del pedido debe ser mayor a 0.");

            RuleFor(x => x.id_producto)
                .GreaterThan(0)
                .WithMessage("El ID del producto debe ser mayor a 0.");

            RuleFor(x => x.id_impresora)
                .GreaterThan(0)
                .WithMessage("El ID de la impresora debe ser mayor a 0.");

            RuleFor(x => x.id_modelo)
                .GreaterThan(0)
                .WithMessage("El ID del modelo 3D debe ser mayor a 0.");

            RuleFor(x => x.id_material)
                .GreaterThan(0)
                .WithMessage("El ID del material debe ser mayor a 0.");

            RuleFor(x => x.cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0.");

            RuleFor(x => x.id_estado)
                .GreaterThan(0)
                .WithMessage("El ID del estado debe ser mayor a 0.");
        }
    }
}
