using System;

namespace impresion3d.Core.Entidades
{
    public class ConsumoMaterial
    {
        public int id_consumo_material { get; set; }
        public int id_trabajo { get; set; }
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public DateTime fecha_hora { get; set; }
    }
}
