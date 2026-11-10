using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class ImpresoraCrearValidator : AbstractValidator<ImpresoraCrearDto>
    {
        public ImpresoraCrearValidator()
        {
            RuleFor(x => x.nombre)
                .NotEmpty().WithMessage("El nombre de la impresora es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de la impresora no puede superar los 100 caracteres.");

            RuleFor(x => x.volumen_x)
                .GreaterThan(0)
                .WithMessage("El volumen X debe ser mayor a 0.");

            RuleFor(x => x.volumen_y)
                .GreaterThan(0)
                .WithMessage("El volumen Y debe ser mayor a 0.");

            RuleFor(x => x.volumen_z)
                .GreaterThan(0)
                .WithMessage("El volumen Z debe ser mayor a 0.");
        }
    }
}
