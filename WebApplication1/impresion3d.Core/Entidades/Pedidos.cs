using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("pedidos")]
    public class Pedidos
    {
        [Column("id_pedido")]
        public int id_pedido { get; set; }

        [Column("id_cliente")]
        public int id_cliente { get; set; }

        [Column("fecha_hora")]
        public DateTime fecha_hora { get; set; }

        [Column("fecha_entrega_estimada")]
        public DateTime? fecha_entrega_estimada { get; set; }

        [Column("fecha_entrega_real")]
        public DateTime? fecha_entrega_real { get; set; }

        [Column("id_estado")]
        public int id_estado { get; set; }

        [Column("total_estimado")]
        public decimal total_estimado { get; set; }

        [Column("subtotal")]
        public decimal subtotal { get; set; }

        [Column("descuento")]
        public decimal descuento { get; set; }

        [Column("total")]
        public decimal total { get; set; }
    }
} 