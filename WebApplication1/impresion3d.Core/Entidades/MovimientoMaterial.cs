using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("movimiento_material")]
    public class MovimientoMaterial
    {
        [Column("id_movimiento_material")]
        public int IdMovimientoMaterial { get; set; }

        [Column("id_material")]
        public int id_material { get; set; }

        [Column("tipo_movimiento")]
        public string tipo_movimiento { get; set; }

        [Column("cantidad")]
        public decimal cantidad { get; set; }

        [Column("fecha_actualizacion")]
        public DateTime fecha_hora { get; set; }

        [Column("observacion")]
        public string observacion { get; set; }
    }
} 