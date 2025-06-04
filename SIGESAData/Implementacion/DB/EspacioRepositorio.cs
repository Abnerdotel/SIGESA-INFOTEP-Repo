
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class EspacioRepositorio : IEspacioRepositorio
    {
        private readonly ConnectionStrings con;

        public EspacioRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<List<Espacio>> Lista()
        {
            var lista = new List<Espacio>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEspacio", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Espacio
                        {
                            IdEspacio = Convert.ToInt32(dr["IdEspacio"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Capacidad = Convert.ToInt32(dr["Capacidad"]),
                            Observaciones = dr["Observaciones"]?.ToString(),
                            FechaCreacion = (DateTime)dr["FechaCreacion"],
                            Tipo = new TipoEspacio
                            {
                                IdTipoEspacio = Convert.ToInt32(dr["IdTipoEspacio"]),
                                Nombre = dr["NombreTipo"].ToString()!,
                                FechaCreacion = (DateTime)dr["FechaCreacionTipo"]
                            }
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<Espacio?> ObtenerPorId(int id)
        {
            Espacio? espacio = null;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEspacioPorId", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdEspacio", id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        espacio = new Espacio
                        {
                            IdEspacio = Convert.ToInt32(dr["IdEspacio"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Capacidad = Convert.ToInt32(dr["Capacidad"]),
                            Observaciones = dr["Observaciones"]?.ToString(),
                            FechaCreacion = (DateTime)dr["FechaCreacion"],
                            Tipo = new TipoEspacio
                            {
                                IdTipoEspacio = Convert.ToInt32(dr["IdTipoEspacio"]),
                                Nombre = dr["NombreTipo"].ToString()!,
                                FechaCreacion = (DateTime)dr["FechaCreacionTipo"]
                            }
                        };
                    }
                }
            }

            return espacio;
        }

        public async Task<string> Guardar(Espacio objeto)
        {
            string mensaje = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_guardarEspacio", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Capacidad", objeto.Capacidad);
                cmd.Parameters.AddWithValue("@IdTipoEspacio", objeto.Tipo.IdTipoEspacio);
                cmd.Parameters.AddWithValue("@Observaciones", (object?)objeto.Observaciones ?? DBNull.Value);

                var outputMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputMensaje);

                await cmd.ExecuteNonQueryAsync();
                mensaje = outputMensaje.Value.ToString()!;
            }

            return mensaje;
        }

        public async Task<string> Editar(Espacio objeto)
        {
            string mensaje = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_editarEspacio", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdEspacio", objeto.IdEspacio);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Capacidad", objeto.Capacidad);
                cmd.Parameters.AddWithValue("@IdTipoEspacio", objeto.Tipo.IdTipoEspacio);
                cmd.Parameters.AddWithValue("@Observaciones", (object?)objeto.Observaciones ?? DBNull.Value);

                var outputMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputMensaje);

                await cmd.ExecuteNonQueryAsync();
                mensaje = outputMensaje.Value.ToString()!;
            }

            return mensaje;
        }

        public async Task<int> Eliminar(int id)
        {
            int resultado = 0;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_eliminarEspacio", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdEspacio", id);

                resultado = await cmd.ExecuteNonQueryAsync();
            }

            return resultado;
        }

        public async Task<List<Espacio>> ListarPorTipo(int idTipoEspacio)
        {
            var lista = new List<Espacio>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEspacioPorTipo", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdTipoEspacio", idTipoEspacio);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Espacio
                        {
                            IdEspacio = Convert.ToInt32(dr["IdEspacio"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Capacidad = Convert.ToInt32(dr["Capacidad"]),
                            Observaciones = dr["Observaciones"]?.ToString(),
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            Tipo = new TipoEspacio
                            {
                                IdTipoEspacio = Convert.ToInt32(dr["IdTipoEspacio"]),
                                Nombre = dr["NombreTipo"].ToString()!,
                                FechaCreacion = (DateTime)dr["FechaCreacionTipo"]
                            }
                        });
                    }
                }
            }

            return lista;
        }
    }
}