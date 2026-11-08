using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("detalle_pedido")]
    public class DetallePedido
    {
        [Column("id_detalle_pedido")]
        public int id_detalle_pedido { get; set; }

        [Column("id_pedido")]
        public int id_pedido { get; set; }

        [Column("id_producto")]
        public int id_producto { get; set; }

        [Column("cantidad")]
        public int cantidad { get; set; }

        [Column("precio_unitario")]
        public decimal precio_unitario { get; set; }

        [Column("subtotal")]
        public decimal subtotal { get; set; }
    }
} 