using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class DetallePedido
    {
        public int IdDetallePedido { get; set; }

        public int id_pedido { get; set; }

        public int id_producto { get; set; }

        public int cantidad { get; set; }

        public decimal precio_unitario { get; set; }

        public decimal subtotal { get; set; } 
    }
}
