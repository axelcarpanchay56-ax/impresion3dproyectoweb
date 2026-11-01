namespace impresion3d.Api.DTOs
{
    public class CrearProductoDto
    {
        public int id_modelo3d { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public decimal precio_venta { get; set; }
    }
} 