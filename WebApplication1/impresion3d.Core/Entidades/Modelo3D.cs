using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("modelo3d")]
    public class Modelo3D
    {
        [Column("id_modelo3d")]
        public int IdModelo3d { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("archivo")]
        public string archivo { get; set; }

        [Column("formato")]
        public string formato { get; set; }

        [Column("descripcion")]
        public string descripcion { get; set; }

        [Column("fecha_creacion")]
        public DateTime fecha_creacion { get; set; }
    }
} 