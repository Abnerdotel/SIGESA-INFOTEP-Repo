

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class BitacoraRepositorio : IBitacoraRepositorio
    {
        private readonly ConnectionStrings con;

        public BitacoraRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<string> Registrar(Bitacora objeto)
        {
            string respuesta = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_registrarBitacora", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Modulo", objeto.Modulo);
                cmd.Parameters.AddWithValue("@Accion", objeto.Accion);
                cmd.Parameters.AddWithValue("@Detalle", objeto.Detalle);
                cmd.Parameters.AddWithValue("@UsuarioAccion", objeto.Accion);
                cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@MsgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al registrar bitácora";
                }
            }

            return respuesta;
        }

        public async Task<List<Bitacora>> Lista(string? modulo = null, string? usuario = null, DateTime? desde = null, DateTime? hasta = null)
        {
            var lista = new List<Bitacora>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_listarBitacora", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Modulo", (object?)modulo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UsuarioAccion", (object?)usuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaDesde", (object?)desde ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaHasta", (object?)hasta ?? DBNull.Value);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Bitacora
                        {
                            IdBitacora = Convert.ToInt32(dr["IdBitacora"]),
                            Modulo = dr["Modulo"].ToString()!,
                            Accion = dr["Accion"].ToString()!,
                            Detalle = dr["Detalle"].ToString()!,
                            FechaAccion = Convert.ToDateTime(dr["FechaAccion"])                        
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<Bitacora?> ObtenerPorId(int id)
        {
            Bitacora? registro = null;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_obtenerBitacoraPorId", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdBitacora", id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        registro = new Bitacora
                        {
                            IdBitacora = Convert.ToInt32(dr["IdBitacora"]),
                            Modulo = dr["Modulo"].ToString()!,
                            Accion = dr["Accion"].ToString()!,
                            Detalle = dr["Detalle"].ToString()!,
                            FechaAccion = Convert.ToDateTime(dr["FechaAccion"]),                        
                       };
                    }
                }
            }

            return registro;
        }


        
        public async Task<int> Eliminar(int id)
        {
            int respuesta = 1;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();

                SqlCommand cmd = new SqlCommand("sp_eliminarBitacora", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdBitacora", id);

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch
                {
                    respuesta = 0;
                }
            }

            return respuesta;
        }
    }
}