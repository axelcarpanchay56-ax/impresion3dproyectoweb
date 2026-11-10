using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class ImpresoraMaterialCrearValidator : AbstractValidator<ImpresoraMaterialCrearDto>
    {
        public ImpresoraMaterialCrearValidator()
        {
            RuleFor(x => x.id_impresora)
                .GreaterThan(0)
                .WithMessage("El ID de la impresora debe ser mayor a 0.");

            RuleFor(x => x.id_material)
                .GreaterThan(0)
                .WithMessage("El ID del material debe ser mayor a 0.");
        }
    }
}
