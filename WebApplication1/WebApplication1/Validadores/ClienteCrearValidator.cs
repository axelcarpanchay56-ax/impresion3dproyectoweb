using FluentValidation;
using impresion3d.Api.DTOs;

namespace impresion3d.Api.Validadores
{
    public class ClienteCrearValidator : AbstractValidator<CrearClienteDto>
    {
        public ClienteCrearValidator()
        {
            RuleFor(x => x.IdPersona)
                .GreaterThan(0)
                .WithMessage("El ID de la persona debe ser mayor a 0.");
        }
    }
}
