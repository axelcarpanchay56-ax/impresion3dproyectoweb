using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class UsuarioCrearValidator : AbstractValidator<CrearUsuarioDto>
    {
        public UsuarioCrearValidator()
        {
            RuleFor(x => x.IdPersona)
                .GreaterThan(0)
                .WithMessage("El ID de la persona debe ser mayor a 0.");

            RuleFor(x => x.Usuario)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre de usuario no puede superar los 50 caracteres.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
