using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class ConsumoMaterialCrearValidator : AbstractValidator<ConsumoMaterialCrearDto>
    {
        public ConsumoMaterialCrearValidator()
        {
            RuleFor(x => x.id_trabajo)
                .GreaterThan(0)
                .WithMessage("El ID del trabajo de impresión debe ser mayor a 0.");

            RuleFor(x => x.id_material)
                .GreaterThan(0)
                .WithMessage("El ID del material debe ser mayor a 0.");

            RuleFor(x => x.cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad de material consumido debe ser mayor a 0.");
        }
    }
}
