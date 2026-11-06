using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("estado_impresion")]
    internal class EstadoImpresion
    {
        [Column("id_estado_impresion")]
        public int IdEstadoImpresion { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }
    }
} 
