using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("consumo_material")]
    internal class ConsumoMaterial
    {
        [Column("id_consumo_material")]
        public int IdConsumoMaterial { get; set; }

        [Column("id_trabajo_impresion")]
        public int id_trabajo_impresion { get; set; }

        [Column("id_material")]
        public int id_material { get; set; }

        [Column("cantidad_real")]
        public decimal cantidad_real { get; set; }

        [Column("fecha_hora")]
        public DateTime fecha_hora { get; set; }
    }
} 