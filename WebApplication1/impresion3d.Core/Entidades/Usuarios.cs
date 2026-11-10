using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace impresion3d.Core.Entidades
{
    [Table("usuarios")]
    public class Usuarios
    {
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_persona")]
        public int id_persona { get; set; }

        [Column("usuario")]
        public string usuario { get; set; } = string.Empty;

        [Column("password_hash")]
        public string password_hash { get; set; } = string.Empty;

        [Column("activo")]
        public bool activo { get; set; }

        [Column("fecha_creacion")]
        public DateTime fecha_creacion { get; set; }

        [Column("ultimo_acceso")]
        public DateTime? ultimo_acceso { get; set; }
    }
} 