using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("impresoras")]
    internal class Impresoras
    {
        [Column("id_impresora")]
        public int IdImpresora { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("marca")]
        public string marca { get; set; }

        [Column("modelo")]
        public string modelo { get; set; }

        [Column("volumen_x")]
        public decimal volumen_x { get; set; }

        [Column("volumen_y")]
        public decimal volumen_y { get; set; }

        [Column("volumen_z")]
        public decimal volumen_z { get; set; }

        [Column("tecnologia")]
        public string tecnologia { get; set; }

        [Column("estado")]
        public bool estado { get; set; }

        [Column("fecha_alta")]
        public DateTime fecha_alta { get; set; }
    }
} 