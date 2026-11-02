namespace impresion3d.Core.Entidades
{
    public class Materiales
    {
        public int id_material { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string? tipo { get; set; }
        public string? unidad_medida { get; set; }
        public string? color { get; set; }
        public decimal? diametro { get; set; }
        public bool activo { get; set; } = true;
    }
}
