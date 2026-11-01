namespace impresion3d.Api.DTOs
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }

        public int id_modelo3d { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public decimal precio_venta { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_alta { get; set; }
    }
} 
