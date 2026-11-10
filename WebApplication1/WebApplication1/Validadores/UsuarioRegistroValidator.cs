using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class UsuarioRegistroValidator : AbstractValidator<RegistroUsuarioDto>
    {
        public UsuarioRegistroValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no puede superar los 50 caracteres.");

            RuleFor(x => x.Dni)
                .NotEmpty().WithMessage("El DNI es obligatorio.")
                .Matches(@"^\d{7,10}$").WithMessage("El DNI debe contener entre 7 y 10 dígitos.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("El email no tiene un formato válido.");

            RuleFor(x => x.Telefono)
                .Matches(@"^\+?\d{6,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Telefono))
                .WithMessage("El teléfono debe contener entre 6 y 15 dígitos.");

            RuleFor(x => x.Usuario)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MinimumLength(3).WithMessage("El usuario debe tener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El usuario no puede superar los 50 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(4).WithMessage("La contraseña debe tener al menos 4 caracteres.");
        }
    }
}
