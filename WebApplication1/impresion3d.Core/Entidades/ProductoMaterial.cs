using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("producto_material")]
    public class ProductoMaterial
    {
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("id_material")]
        public int IdMaterial { get; set; }

        [Column("cantidad_estimada")]
        public int cantidad_estimada { get; set; }
    }
} 