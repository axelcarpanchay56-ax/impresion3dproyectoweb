using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class Modelo3DCrearValidator : AbstractValidator<Modelo3DCrearDto>
    {
        public Modelo3DCrearValidator()
        {
            RuleFor(x => x.nombre)
                .NotEmpty().WithMessage("El nombre del modelo 3D es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre del modelo 3D no puede superar los 100 caracteres.");

            RuleFor(x => x.archivo)
                .NotEmpty()
                .WithMessage("El archivo del modelo 3D es obligatorio.");

            RuleFor(x => x.formato)
                .NotEmpty()
                .WithMessage("El formato del modelo 3D es obligatorio.");
        }
    }
}
