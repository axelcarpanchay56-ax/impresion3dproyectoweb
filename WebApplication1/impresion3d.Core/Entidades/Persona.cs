using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("personas")]
    public class Persona
    {
        [Column("id_persona")]
        public int IdPersona { get; set; }

        [Column("dni")]
        public string Dni { get; set; } = string.Empty;

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("direccion")]
        public string? Direccion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("fecha_alta")]
        public DateTime FechaAlta { get; set; }
    }
} 