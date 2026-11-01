using System;

namespace impresion3d.Api.DTOs
{
    public class ImpresoraDto
    {
        public int id_impresora { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string marca { get; set; } = string.Empty;
        public string modelo { get; set; } = string.Empty;
        public decimal volumen_x { get; set; }
        public decimal volumen_y { get; set; }
        public decimal volumen_z { get; set; }
        public string tecnologia { get; set; } = string.Empty;
        public bool estado { get; set; }
        public DateTime fecha_alta { get; set; }
    }

    public class ImpresoraCrearDto
    {
        public string nombre { get; set; } = string.Empty;
        public string marca { get; set; } = string.Empty;
        public string modelo { get; set; } = string.Empty;
        public decimal volumen_x { get; set; }
        public decimal volumen_y { get; set; }
        public decimal volumen_z { get; set; }
        public string tecnologia { get; set; } = string.Empty;
        public bool estado { get; set; }
        public DateTime fecha_alta { get; set; }
    }

    public class ImpresoraActualizarDto
    {
        public int id_impresora { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string marca { get; set; } = string.Empty;
        public string modelo { get; set; } = string.Empty;
        public decimal volumen_x { get; set; }
        public decimal volumen_y { get; set; }
        public decimal volumen_z { get; set; }
        public string tecnologia { get; set; } = string.Empty;
        public bool estado { get; set; }
    }
}
