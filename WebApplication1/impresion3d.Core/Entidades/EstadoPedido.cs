using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("estado_pedido")]
    public class EstadoPedido
    {
        [Column("id_estado_pedido")]
        public int id_estado_pedido { get; set; }

        [Column("nombre")]
        public string nombre { get; set; } = string.Empty;
    }
} 
