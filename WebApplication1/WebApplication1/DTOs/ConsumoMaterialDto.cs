using System;

namespace impresion3d.Api.DTOs
{
    public class ConsumoMaterialDto
    {
        public int id_consumo_material { get; set; }
        public int id_trabajo { get; set; }
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public DateTime fecha_hora { get; set; }
    }

    public class ConsumoMaterialCrearDto
    {
        public int id_trabajo { get; set; }
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public DateTime? fecha_hora { get; set; }
    }

    public class ConsumoMaterialActualizarDto
    {
        public int id_consumo_material { get; set; }
        public int id_trabajo { get; set; }
        public int id_material { get; set; }
        public decimal cantidad { get; set; }
        public DateTime fecha_hora { get; set; }
    }
}
