using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class EstadoImpresionCrearValidator : AbstractValidator<EstadoImpresionCrearDto>
    {
        public EstadoImpresionCrearValidator()
        {
            RuleFor(x => x.nombre)
                .NotEmpty().WithMessage("El nombre del estado de impresión es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre del estado de impresión no puede superar los 50 caracteres.");
        }
    }
}
