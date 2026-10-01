using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class ConsumoMaterial
    {
        public int IdConsumoMaterial { get; set; }

        public int id_trabajo_impresion { get; set; }

        public int id_material { get; set; }

        public decimal cantidad_real { get; set; }

        public DateTime fecha_hora { get; set; }
    }
}
