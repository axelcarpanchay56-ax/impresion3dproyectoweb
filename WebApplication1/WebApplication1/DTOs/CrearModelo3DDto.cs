namespace impresion3d.Api.DTOs
{
    public class CrearModelo3DDto
    {
        public string nombre { get; set; }

        public string archivo { get; set; }

        public string formato { get; set; }

        public string descripcion { get; set; }

        public DateTime fecha_creacion { get; set; }
    }
} 