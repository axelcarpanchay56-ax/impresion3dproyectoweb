namespace impresion3d.Api.DTOs
{
    public class MaterialDto
    {
        public int IdMaterial { get; set; }

        public string nombre { get; set; }

        public string tipo { get; set; }

        public string unidad_medida { get; set; }

        public string color { get; set; }

        public decimal diametro { get; set; }

        public bool activo { get; set; }
    }
} 