using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SigesaData.Implementacion.DB
{
    public class RolRepositorio : IRolRepositorio
    {

        private readonly ConnectionStrings _con;

        public RolRepositorio(IOptions<ConnectionStrings> options)
        {
            _con = options.Value;
        }

        public async Task<List<Rol>> Lista()
        {
            var lista = new List<Rol>();
            using var conexion = new SqlConnection(_con.CadenaSQL);
            await conexion.OpenAsync();
            var cmd = new SqlCommand("sp_listaRol", conexion) { CommandType = CommandType.StoredProcedure };

            using var dr = await cmd.ExecuteReaderAsync();
            while (await dr.ReadAsync())
            {
                lista.Add(new Rol
                {
                    IdRol = Convert.ToInt32(dr["IdRol"]),
                    Nombre = dr["Nombre"].ToString()!,
                    FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                });
            }

            return lista;
        }

        public async Task<Rol?> ObtenerPorId(int id)
        {
            Rol? rol = null;
            using var conexion = new SqlConnection(_con.CadenaSQL);
            await conexion.OpenAsync();
            var cmd = new SqlCommand("sp_obtenerRolPorId", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@IdRol", id);

            using var dr = await cmd.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                rol = new Rol
                {
                    IdRol = Convert.ToInt32(dr["IdRol"]),
                    Nombre = dr["Nombre"].ToString()!,
                    FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                };
            }

            return rol;
        }

        public async Task<string> Guardar(Rol objeto)
        {
            string respuesta;
            using var conexion = new SqlConnection(_con.CadenaSQL);
            await conexion.OpenAsync();
            var cmd = new SqlCommand("sp_guardarRol", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
            cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();
            respuesta = cmd.Parameters["@MsgError"].Value?.ToString()!;
            return respuesta;
        }

        public async Task<string> Editar(Rol objeto)
        {
            string respuesta;
            using var conexion = new SqlConnection(_con.CadenaSQL);
            await conexion.OpenAsync();
            var cmd = new SqlCommand("sp_editarRol", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@IdRol", objeto.IdRol);
            cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
            cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            await cmd.ExecuteNonQueryAsync();
            respuesta = cmd.Parameters["@MsgError"].Value?.ToString()!;
            return respuesta;
        }

        public async Task<int> Eliminar(int id)
        {
            int resultado = 1;
            using var conexion = new SqlConnection(_con.CadenaSQL);
            await conexion.OpenAsync();
            var cmd = new SqlCommand("sp_eliminarRol", conexion)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@IdRol", id);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch
            {
                resultado = 0;
            }

            return resultado;
        }
    }
}




