using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class DetallePedidoCrearValidator : AbstractValidator<DetallePedidoCrearDto>
    {
        public DetallePedidoCrearValidator()
        {
            RuleFor(x => x.id_pedido)
                .GreaterThan(0)
                .WithMessage("El ID del pedido debe ser mayor a 0.");

            RuleFor(x => x.id_producto)
                .GreaterThan(0)
                .WithMessage("El ID del producto debe ser mayor a 0.");

            RuleFor(x => x.cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0.");

            RuleFor(x => x.precio_unitario)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El precio unitario no puede ser negativo.");
        }
    }
}
