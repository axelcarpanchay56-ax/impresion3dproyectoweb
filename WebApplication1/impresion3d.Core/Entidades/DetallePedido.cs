namespace impresion3d.Core.Entidades
{
    public class DetallePedido
    {
        public int id_detalle_pedido { get; set; }
        public int id_pedido { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal subtotal { get; set; }
    }
}
