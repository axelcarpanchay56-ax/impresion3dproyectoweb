using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("estado_pedido")]
    internal class EstadoPedido
    {
        [Column("id_estado_pedido")]
        public int IdEstadoPedido { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }
    }
} 