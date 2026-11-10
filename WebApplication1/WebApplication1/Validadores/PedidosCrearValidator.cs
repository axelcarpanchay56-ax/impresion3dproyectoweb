using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class PedidosCrearValidator : AbstractValidator<PedidosCrearDto>
    {
        public PedidosCrearValidator()
        {
            RuleFor(x => x.id_cliente)
                .GreaterThan(0)
                .WithMessage("El ID del cliente debe ser mayor a 0.");

            RuleFor(x => x.id_estado)
                .GreaterThan(0)
                .WithMessage("El ID del estado debe ser mayor a 0.");

            RuleFor(x => x.total)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El total no puede ser negativo.");
        }
    }
}
