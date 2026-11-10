using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class EstadoPedidoCrearValidator : AbstractValidator<EstadoPedidoCrearDto>
    {
        public EstadoPedidoCrearValidator()
        {
            RuleFor(x => x.nombre)
                .NotEmpty().WithMessage("El nombre del estado de pedido es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del estado de pedido no puede superar los 50 caracteres.");
        }
    }
}
