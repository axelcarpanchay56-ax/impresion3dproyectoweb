using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class UsuarioRolAsignarValidator : AbstractValidator<AsignarUsuarioRolDto>
    {
        public UsuarioRolAsignarValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0)
                .WithMessage("El ID del usuario debe ser mayor a 0.");

            RuleFor(x => x.IdRol)
                .GreaterThan(0)
                .WithMessage("El ID del rol debe ser mayor a 0.");
        }
    }
}
