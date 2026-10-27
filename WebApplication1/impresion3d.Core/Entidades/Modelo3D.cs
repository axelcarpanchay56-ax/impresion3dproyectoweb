using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    public class Modelo3D
    {
        public int IdModelo3d { get; set; }

        public string nombre { get; set; }

        public string archivo { get; set; }

        public string formato { get; set; }

        public string descripcion { get; set; }

        public DateTime fecha_creacion { get; set; }


    }
}
