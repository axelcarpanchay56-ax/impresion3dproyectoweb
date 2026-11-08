using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace impresion3d.Core.Interfaces
{
    public interface IMiConexion
    {
        IDbConnection ObtenerConexion();
        Task<IDbConnection> ObtenerConexionAsync();
    }
}
