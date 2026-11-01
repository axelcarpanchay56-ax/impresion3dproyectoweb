using impresion3d.Core.Entidades;
using impresion3d.Infrastructure.Contexto;
using impresion3d.Infrastructure.Repositorios;
using Xunit;

namespace impresion3d.Test
{
    public class TestClienteRepositorio
    {
        [Fact]
        public async Task ObtenerTodosAsync_DeberiaTraerClientes()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);
            var repositorio = new ClienteRepositorio(conexion);

            var clientes = await repositorio.ObtenerTodosAsync();

            Assert.NotNull(clientes);
            Assert.True(clientes.Count > 0);
        }
        [Fact]
        public async Task CrearAsync_DeberiaCrearCliente()
        {
            string connectionString =
                "Server=127.0.0.1;Database=db_impresiones3d;User Id=root;Password=root;Port=3306;";

            var conexion = new MyConexion(connectionString);

            var personaRepositorio =
                new PersonaRepositorio(conexion);

            var clienteRepositorio =
                new ClienteRepositorio(conexion);

            string dniAleatorio =
                Random.Shared.Next(10000000, 99999999).ToString();

            var nuevaPersona = new Persona
            {
                Dni = dniAleatorio,
                Nombre = "Cliente",
                Apellido = "Prueba",
                Telefono = "3815550000",
                Email = "cliente.prueba@gmail.com",
                Direccion = "Calle Prueba 123",
                Activo = true,
                FechaAlta = DateTime.Now
            };

            await personaRepositorio.CrearAsync(nuevaPersona);

            var personaCreada =
                await personaRepositorio.ObtenerPorDniAsync(dniAleatorio);

            Assert.NotNull(personaCreada);

            var nuevoCliente = new Clientes
            {
                id_persona = personaCreada.IdPersona
            };

            int resultado =
                await clienteRepositorio.CrearAsync(nuevoCliente);

            Assert.True(resultado > 0);
        } 
    }
} 