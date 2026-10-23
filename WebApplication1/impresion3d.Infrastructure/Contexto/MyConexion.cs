using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using System.Data;
using impresion3d.Core.Interfaces;
using System.Threading.Tasks;

namespace impresion3d.Infrastructure.Contexto
{
    public class MyConexion : Imiconexion
    {
        private readonly string _cadenaDeConexion;

        public MyConexion(string _cadena)
        {
            _cadenaDeConexion = _cadena;
        }

        public MySqlConnection obtenerConexion()
        {
            MySqlConnection conn = new MySqlConnection(_cadenaDeConexion);
            conn.Open();
            return conn;
        }

        public async Task<MySqlConnection> obtenerConexionAsync()
        {
            MySqlConnection conn = new MySqlConnection(_cadenaDeConexion);
            await conn.OpenAsync();
            return conn;
        }
        IDbConnection Imiconexion.obtenerConexion()
        {
            return obtenerConexion();
        }

        async Task<IDbConnection> Imiconexion.obtenerConexionAsync()
        {
            var conn = await obtenerConexionAsync();
            return conn;
        }
    }
}

