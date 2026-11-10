using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class MaterialesCrearValidator : AbstractValidator<MaterialesCrearDto>
    {
        public MaterialesCrearValidator()
        {
            RuleFor(x => x.nombre)
                .NotEmpty().WithMessage("El nombre del material es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del material no puede superar los 100 caracteres.");

            RuleFor(x => x.diametro)
                .GreaterThan(0)
                .When(x => x.diametro.HasValue)
                .WithMessage("El diámetro debe ser mayor a 0.");
        }
    }
}
