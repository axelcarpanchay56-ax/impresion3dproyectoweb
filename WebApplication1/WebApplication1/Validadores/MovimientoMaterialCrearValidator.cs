using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class MovimientoMaterialCrearValidator : AbstractValidator<CrearMovimientoMaterialDto>
    {
        public MovimientoMaterialCrearValidator()
        {
            RuleFor(x => x.id_material)
                .GreaterThan(0)
                .WithMessage("El ID del material debe ser mayor a 0.");

            RuleFor(x => x.tipo_movimiento)
                .NotEmpty()
                .WithMessage("El tipo de movimiento es obligatorio.");

            RuleFor(x => x.cantidad)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a 0.");
        }
    }
}
