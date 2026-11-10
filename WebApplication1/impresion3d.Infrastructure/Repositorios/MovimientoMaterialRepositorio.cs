using impresion3d.Core.Entidades;
using impresion3d.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Dapper;

namespace impresion3d.Infrastructure.Repositorios
{
    public class MovimientoMaterialRepositorio : IMovimientoMaterialRepositorio
    {
        private readonly IMiConexion _conexion;

        public MovimientoMaterialRepositorio(IMiConexion conexion)
        {
            _conexion = conexion;

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            SqlMapper.SetTypeMap(
                typeof(MovimientoMaterial),
                new CustomPropertyTypeMap(
                    typeof(MovimientoMaterial),
                    (type, columnName) =>
                        type.GetProperties().FirstOrDefault(prop =>
                            prop.GetCustomAttributes(false)
                                .OfType<ColumnAttribute>()
                                .Any(attr => string.Equals(
                                    attr.Name,
                                    columnName,
                                    StringComparison.OrdinalIgnoreCase
                                ))
                            || string.Equals(
                                prop.Name,
                                columnName,
                                StringComparison.OrdinalIgnoreCase
                            )
                            || string.Equals(
                                prop.Name,
                                columnName.Replace("_", ""),
                                StringComparison.OrdinalIgnoreCase
                            )
                        )!
                )
            );
        }

        public async Task<List<MovimientoMaterial>> ObtenerTodosAsync()
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var lista =
                await conexion.QueryAsync<MovimientoMaterial>(
                    "sp_movimiento_material_obtener_todos",
                    commandType: CommandType.StoredProcedure
                );

            return lista.ToList();
        }

        public async Task<MovimientoMaterial?> ObtenerPorIdAsync(int id)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_movimiento_material",
                id,
                DbType.Int32,
                ParameterDirection.Input
            );

            var movimiento =
                await conexion.QueryFirstOrDefaultAsync<MovimientoMaterial>(
                    "sp_movimiento_material_obtener_por_id",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return movimiento;
        }

        public async Task<int> CrearAsync(MovimientoMaterial movimientoMaterial)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_material", movimientoMaterial.id_material);
            parametros.Add("p_tipo_movimiento", movimientoMaterial.tipo_movimiento);
            parametros.Add("p_cantidad", movimientoMaterial.cantidad);
            parametros.Add("p_fecha_actualizacion", movimientoMaterial.fecha_hora);
            parametros.Add("p_observacion", movimientoMaterial.observacion);

            var resultado =
                await conexion.ExecuteScalarAsync<int>(
                    "sp_movimiento_material_crear",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return resultado;
        }

        public async Task<bool> ActualizarAsync(MovimientoMaterial movimientoMaterial)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add("p_id_movimiento_material", movimientoMaterial.IdMovimientoMaterial);
            parametros.Add("p_id_material", movimientoMaterial.id_material);
            parametros.Add("p_tipo_movimiento", movimientoMaterial.tipo_movimiento);
            parametros.Add("p_cantidad", movimientoMaterial.cantidad);
            parametros.Add("p_fecha_actualizacion", movimientoMaterial.fecha_hora);
            parametros.Add("p_observacion", movimientoMaterial.observacion);

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_movimiento_material_actualizar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conexion =
                await _conexion.ObtenerConexionAsync();

            var parametros = new DynamicParameters();

            parametros.Add(
                "p_id_movimiento_material",
                id,
                DbType.Int32,
                ParameterDirection.Input
            );

            int filasAfectadas =
                await conexion.ExecuteAsync(
                    "sp_movimiento_material_eliminar",
                    parametros,
                    commandType: CommandType.StoredProcedure
                );

            return filasAfectadas > 0;
        }
    }
}
