using System;

namespace impresion3d.Core.Entidades
{
    public class Modelo3D
    {
        public int id_modelo3d { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string archivo { get; set; } = string.Empty;
        public string formato { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public DateTime fecha_creacion { get; set; }
    }
}
