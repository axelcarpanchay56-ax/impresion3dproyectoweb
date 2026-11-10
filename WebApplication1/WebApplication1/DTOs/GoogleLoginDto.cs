namespace impresion3d.Api.DTOs
{
    public class GoogleLoginDto
    {
        public string? Credential { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string? GoogleId { get; set; }
    }
}
