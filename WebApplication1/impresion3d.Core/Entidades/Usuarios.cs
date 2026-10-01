using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class Usuarios
    {
        public int IdUsuario { get; set; }

        public int id_persona { get; set; }

        public string usuario { get; set; }

        public string password_hash { get; set; }

        public bool activo { get; set; }

        public DateTime fecha_creacion { get; set; }

        public DateTime ultimo_acceso { get; set; }

    }
}
