using System;

namespace impresion3d.Api.DTOs
{
    public class Modelo3DDto
    {
        public int id_modelo3d { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string archivo { get; set; } = string.Empty;
        public string formato { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public DateTime fecha_creacion { get; set; }
    }

    public class Modelo3DCrearDto
    {
        public string nombre { get; set; } = string.Empty;
        public string archivo { get; set; } = string.Empty;
        public string formato { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
    }

    public class Modelo3DActualizacionDto
    {
        public int id_modelo3d { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string archivo { get; set; } = string.Empty;
        public string formato { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
    }
}
