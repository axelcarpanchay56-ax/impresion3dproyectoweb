using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("impresoras")]
    public class Impresoras
    {
        [Column("id_impresora")]
        public int id_impresora { get; set; }

        [Column("nombre")]
        public string nombre { get; set; } = string.Empty;

        [Column("marca")]
        public string marca { get; set; } = string.Empty;

        [Column("modelo")]
        public string modelo { get; set; } = string.Empty;

        [Column("volumen_x")]
        public decimal volumen_x { get; set; }

        [Column("volumen_y")]
        public decimal volumen_y { get; set; }

        [Column("volumen_z")]
        public decimal volumen_z { get; set; }

        [Column("tecnologia")]
        public string tecnologia { get; set; } = string.Empty;

        [Column("estado")]
        public bool estado { get; set; }

        [Column("fecha_alta")]
        public DateTime fecha_alta { get; set; }
    }
} 