namespace impresion3d.Api.DTOs
{
    public class MaterialesDto
    {
        public int id_material { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? tipo { get; set; }
        public string? unidad_medida { get; set; }
        public string? color { get; set; }
        public decimal? diametro { get; set; }
        public bool activo { get; set; }
    }

    public class MaterialesCrearDto
    {
        public string nombre { get; set; } = string.Empty;
        public string? tipo { get; set; }
        public string? unidad_medida { get; set; }
        public string? color { get; set; }
        public decimal? diametro { get; set; }
        public bool activo { get; set; } = true;
    }

    public class MaterialesActualizarDto
    {
        public int id_material { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? tipo { get; set; }
        public string? unidad_medida { get; set; }
        public string? color { get; set; }
        public decimal? diametro { get; set; }
        public bool activo { get; set; }
    }
}
