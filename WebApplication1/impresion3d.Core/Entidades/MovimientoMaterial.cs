using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class MovimientoMaterial
    {
        public int IdMovimientoMaterial { get; set; }

        public int id_material { get; set; }

        public string tipo_movimiento { get; set; }

        public decimal cantidad { get; set; }

        public DateTime fecha_hora { get; set; }

        public string observacion { get; set; } 
    }
}
