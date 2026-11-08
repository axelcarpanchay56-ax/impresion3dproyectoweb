using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class PersonaCrearValidator : AbstractValidator<CrearPersonaDto>
    {
        public PersonaCrearValidator()
        {
            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede superar los 50 caracteres.");

            RuleFor(x => x.Dni)
                .Matches(@"^\d{7,8}$")
                .When(x => !string.IsNullOrEmpty(x.Dni))
                .WithMessage("El DNI debe contener entre 7 y 8 dígitos.");

            RuleFor(x => x.Telefono)
                .Matches(@"^\+?\d{1,15}$")
                .When(x => !string.IsNullOrEmpty(x.Telefono))
                .WithMessage("El teléfono debe contener solo dígitos y puede incluir un prefijo '+'.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("El email no tiene un formato válido.");

            RuleFor(x => x.Direccion)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Direccion))
                .WithMessage("La dirección no puede superar los 100 caracteres.");
        }
    }
} 
