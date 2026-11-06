using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("usuario_rol")]
    public class Usuario_Rol
    {
        [Column("id_rol")]
        public int id_rol { get; set; }

        [Column("id_usuario")]
        public int id_usuario { get; set; }
    }
} 