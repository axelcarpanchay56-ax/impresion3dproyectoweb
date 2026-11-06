using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("materiales")]
    public class Materiales
    {
        [Column("id_material")]
        public int IdMaterial { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("tipo")]
        public string tipo { get; set; }

        [Column("unidad_medida")]
        public string unidad_medida { get; set; }

        [Column("color")]
        public string color { get; set; }

        [Column("diametro")]
        public decimal diametro { get; set; }

        [Column("activo")]
        public bool activo { get; set; }
    }
} 