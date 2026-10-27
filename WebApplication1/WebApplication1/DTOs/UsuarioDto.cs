namespace impresion3d.Api.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }

        public int IdPersona { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? UltimoAcceso { get; set; }
    }
} 
