using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace impresion3d.Core.Interfaces
{
    public interface Imiconexion
    {
        IDbConnection obtenerConexion();
        Task<IDbConnection> obtenerConexionAsync();
    }
}
