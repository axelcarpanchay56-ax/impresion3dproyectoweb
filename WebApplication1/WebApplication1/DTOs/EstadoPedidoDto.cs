namespace impresion3d.Api.DTOs
{
    public class EstadoPedidoDto
    {
        public int id_estado_pedido { get; set; }
        public string nombre { get; set; } = string.Empty;
    }

    public class EstadoPedidoCrearDto
    {
        public string nombre { get; set; } = string.Empty;
    }

    public class EstadoPedidoActualizarDto
    {
        public int id_estado_pedido { get; set; }
        public string nombre { get; set; } = string.Empty;
    }
}
