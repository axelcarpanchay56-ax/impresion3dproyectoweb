using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("productos")]
    public class Productos
    {
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("id_modelo3d")]
        public int id_modelo3d { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("descripcion")]
        public string descripcion { get; set; }

        [Column("precio_venta")]
        public decimal precio_venta { get; set; }

        [Column("activo")]
        public bool activo { get; set; }

        [Column("fecha_alta")]
        public DateTime fecha_alta { get; set; }
    }
} 