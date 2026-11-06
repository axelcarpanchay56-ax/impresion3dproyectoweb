using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("impresora_material")]
    internal class ImpresoraMaterial
    {
        [Column("id_impresora")]
        public int IdImpresora { get; set; }

        [Column("id_material")]
        public int IdMaterial { get; set; }

        [Column("observacion")]
        public string observacion { get; set; }
    }
} 
