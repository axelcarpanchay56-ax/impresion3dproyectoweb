using System;

namespace impresion3d.Core.Entidades
{
    public class TrabajoImpresion
    {
        public int id_trabajo_impresion { get; set; }
        public int id_pedido { get; set; }
        public int id_producto { get; set; }
        public int id_impresora { get; set; }
        public int id_modelo { get; set; }
        public int id_material { get; set; }
        public int cantidad { get; set; }
        public int id_estado { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public TimeSpan tiempo_estimado { get; set; }
        public decimal peso_estimado { get; set; }
        public string observacion { get; set; } = string.Empty;
    }
}
