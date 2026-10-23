namespace impresion3d.Api.DTOs
{
    public class MovimientoMaterialDto
    {
        public int IdMovimientoMaterial { get; set; }

        public int id_material { get; set; }

        public string tipo_movimiento { get; set; }

        public decimal cantidad { get; set; }

        public DateTime fecha_hora { get; set; }

        public string observacion { get; set; }
    }
} 
