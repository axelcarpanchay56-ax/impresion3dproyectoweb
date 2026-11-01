namespace impresion3d.Api.DTOs
{
    public class LoginUsuarioDto
    {
        public string Usuario { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
} 