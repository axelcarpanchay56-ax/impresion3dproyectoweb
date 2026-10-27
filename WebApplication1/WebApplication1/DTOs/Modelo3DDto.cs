namespace impresion3d.Api.DTOs
{
    public class Modelo3DDto
    {
        public int IdModelo3d { get; set; }

        public string nombre { get; set; }

        public string archivo { get; set; }

        public string formato { get; set; }

        public string descripcion { get; set; }

        public DateTime fecha_creacion { get; set; }
    }
} 
