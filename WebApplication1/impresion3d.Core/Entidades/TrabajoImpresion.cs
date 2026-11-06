using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("trabajo_impresion")]
    internal class TrabajoImpresion
    {
        [Column("id_trabajo_impresion")]
        public int IdTrabajoImpresion { get; set; }

        [Column("id_pedido")]
        public int id_pedido { get; set; }

        [Column("id_producto")]
        public int id_producto { get; set; }

        [Column("id_impresora")]
        public int id_impresora { get; set; }

        [Column("id_modelo")]
        public int id_modelo { get; set; }

        [Column("id_material")]
        public int id_material { get; set; }

        [Column("cantidad")]
        public int cantidad { get; set; }

        [Column("id_estado")]
        public int id_estado { get; set; }

        [Column("fecha_inicio")]
        public DateTime fecha_inicio { get; set; }

        [Column("fecha_fin")]
        public DateTime fecha_fin { get; set; }

        [Column("tiempo_estimado")]
        public TimeSpan tiempo_estimado { get; set; }

        [Column("peso_estimado")]
        public decimal peso_estimado { get; set; }

        [Column("observacion")]
        public string observacion { get; set; }
    }
}  