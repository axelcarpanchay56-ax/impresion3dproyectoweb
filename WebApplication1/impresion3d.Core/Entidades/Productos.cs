using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class Productos
    {
        public int IdProducto { get; set; }

        public int id_modelo3d { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public decimal precio_venta { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_alta { get; set; }
    }
}
