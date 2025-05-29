
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class NotificacionRepositorio : INotificacionRepositorio
    {
        private readonly ConnectionStrings con;

        public NotificacionRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<string> Registrar(Notificacion objeto)
        {
            string respuesta = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_guardarNotificacion", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdUsuario", objeto.Usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@Mensaje", objeto.Mensaje);
                cmd.Parameters.AddWithValue("@IdTipoNotificacion", objeto.Tipo.IdTipoNotificacion);
                cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@MsgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al guardar notificación";
                }
            }

            return respuesta;
        }

        public async Task<List<Notificacion>> ListarPorUsuario(int idUsuario)
        {
            var lista = new List<Notificacion>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaNotificacionesPorUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Notificacion
                        {
                            IdNotificacion = Convert.ToInt32(dr["IdNotificacion"]),
                            Mensaje = dr["Mensaje"].ToString()!,
                            FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]),
                            Tipo = new TipoNotificacion
                            {
                                IdTipoNotificacion = Convert.ToInt32(dr["IdTipoNotificacion"]),
                                Nombre = dr["NombreTipo"].ToString()!
                            },
                            Usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["NombreUsuario"].ToString()!
                            }
                        });
                    }
                }
            }

            return lista;
        }
    }
}
