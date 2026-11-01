namespace impresion3d.Api.DTOs
{
    public class ImpresoraMaterialDto
    {
        public int id_impresora { get; set; }
        public int id_material { get; set; }
        public string? observacion { get; set; }
    }

    public class ImpresoraMaterialCrearDto
    {
        public int id_impresora { get; set; }
        public int id_material { get; set; }
        public string? observacion { get; set; }
    }

    public class ImpresoraMaterialActualizarDto
    {
        public string? observacion { get; set; }
    }
}
