namespace impresion3d.Api.DTOs
{
    public class CrearUsuarioDto
    {
        public int IdPersona { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
} 