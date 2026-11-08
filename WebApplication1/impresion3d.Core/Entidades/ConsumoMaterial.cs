using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("consumo_material")]
    public class ConsumoMaterial
    {
        [Column("id_consumo_material")]
        public int id_consumo_material { get; set; }

        [Column("id_trabajo")]
        public int id_trabajo { get; set; }

        [Column("id_material")]
        public int id_material { get; set; }

        [Column("cantidad")]
        public decimal cantidad { get; set; }

        [Column("fecha_hora")]
        public DateTime fecha_hora { get; set; }
    }
} 