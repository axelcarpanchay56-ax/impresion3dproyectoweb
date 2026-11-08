using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("clientes")]
    public class Clientes
    {
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_persona")]
        public int id_persona { get; set; }
    }
} 