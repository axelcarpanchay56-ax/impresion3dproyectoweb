using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("roles")]
    public class Roles
    {
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("nombre")]
        public string nombre { get; set; }

        [Column("descripcion")]
        public string descripcion { get; set; }
    }
} 