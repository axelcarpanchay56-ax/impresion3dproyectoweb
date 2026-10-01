using System;
using System.Collections.Generic;
using System.Text;

namespace impresion3d.Core.Entidades
{
    internal class Persona
    {
        public int IdPersona { get; set; }

        public string Dni { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Telefono { get; set; }


        public string Email { get; set; }

        public string Direccion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaAlta { get; set; }


    }
}
