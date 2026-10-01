using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class Impresoras
    {
        public int IdImpresora { get; set; }

        public string nombre { get; set; }

        public string marca { get; set; }

        public string modelo { get; set; }

        public decimal volumen_x { get; set; }

        public decimal volumen_y { get; set; }

        public decimal volumen_z { get; set; }

        public string tecnologia { get; set; }

        public bool estado { get; set; }

        public DateTime fecha_alta { get; set; }
    }
}
