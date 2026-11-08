using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("estado_impresion")]
    public class EstadoImpresion
    {
        [Column("id_estado_impresion")]
        public int id_estado_impresion { get; set; }

        [Column("nombre")]
        public string nombre { get; set; } = string.Empty;
    }
} 
