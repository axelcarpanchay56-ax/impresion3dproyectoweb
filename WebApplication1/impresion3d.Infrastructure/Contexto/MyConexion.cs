using System.Data;
using impresion3d.Core.Interfaces;
using MySqlConnector;

namespace impresion3d.Infrastructure.Contexto
{
    public class MyConexion : IMiConexion
    {
        private readonly string _cadenaDeConexion;

        public MyConexion(string cadena)
        {
            _cadenaDeConexion = cadena;
        }

        public IDbConnection ObtenerConexion()
        {
            return new MySqlConnection(_cadenaDeConexion);
        }

        public async Task<IDbConnection> ObtenerConexionAsync()
        {
            var conn = new MySqlConnection(_cadenaDeConexion);
            await conn.OpenAsync();
            return conn;
        }
    }
}

