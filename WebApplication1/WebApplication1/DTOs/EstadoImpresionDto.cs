namespace impresion3d.Api.DTOs
{
    public class EstadoImpresionDto
    {
        public int id_estado_impresion { get; set; }
        public string nombre { get; set; } = string.Empty;
    }

    public class EstadoImpresionCrearDto
    {
        public string nombre { get; set; } = string.Empty;
    }

    public class EstadoImpresionActualizarDto
    {
        public int id_estado_impresion { get; set; }
        public string nombre { get; set; } = string.Empty;
    }
}
