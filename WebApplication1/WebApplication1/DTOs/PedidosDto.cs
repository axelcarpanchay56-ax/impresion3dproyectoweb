using System;

namespace impresion3d.Api.DTOs
{
    public class PedidosDto
    {
        public int id_pedido { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha_hora { get; set; }
        public DateTime? fecha_entrega_estimada { get; set; }
        public DateTime? fecha_entrega_real { get; set; }
        public int id_estado { get; set; }
        public decimal total_estimado { get; set; }
        public decimal subtotal { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
    }

    public class PedidosCrearDto
    {
        public int id_cliente { get; set; }
        public DateTime? fecha_hora { get; set; }
        public DateTime? fecha_entrega_estimada { get; set; }
        public DateTime? fecha_entrega_real { get; set; }
        public int id_estado { get; set; }
        public decimal total_estimado { get; set; }
        public decimal subtotal { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
    }

    public class PedidosActualizarDto
    {
        public int id_pedido { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha_hora { get; set; }
        public DateTime? fecha_entrega_estimada { get; set; }
        public DateTime? fecha_entrega_real { get; set; }
        public int id_estado { get; set; }
        public decimal total_estimado { get; set; }
        public decimal subtotal { get; set; }
        public decimal descuento { get; set; }
        public decimal total { get; set; }
    }
}
