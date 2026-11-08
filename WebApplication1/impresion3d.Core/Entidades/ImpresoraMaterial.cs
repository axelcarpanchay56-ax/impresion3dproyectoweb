using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("impresora_material")]
    public class ImpresoraMaterial
    {
        [Column("id_impresora")]
        public int id_impresora { get; set; }

        [Column("id_material")]
        public int id_material { get; set; }

        [Column("observacion")]
        public string? observacion { get; set; }
    }
} 