namespace impresion3d.Api.DTOs
{
    public class RegistroUsuarioDto
    {
        public string Dni { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
