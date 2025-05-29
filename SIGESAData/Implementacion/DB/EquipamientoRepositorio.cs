
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class EquipamientoRepositorio : IEquipamientoRepositorio
    {
        private readonly ConnectionStrings con;

        public EquipamientoRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<List<Equipamiento>> Lista()
        {
            var lista = new List<Equipamiento>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEquipamiento", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Equipamiento
                        {
                            IdEquipamiento = Convert.ToInt32(dr["IdEquipamiento"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Descripcion = dr["Descripcion"].ToString()!,
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<Equipamiento?> ObtenerPorId(int id)
        {
            Equipamiento? objeto = null;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEquipamientoPorId", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdEquipamiento", id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        objeto = new Equipamiento
                        {
                            IdEquipamiento = Convert.ToInt32(dr["IdEquipamiento"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Descripcion = dr["Descripcion"].ToString()!,
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                        };
                    }
                }
            }

            return objeto;
        }

        public async Task<string> Guardar(Equipamiento objeto)
        {
            string respuesta = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_guardarEquipamiento", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@MsgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al guardar equipamiento";
                }
            }

            return respuesta;
        }

        public async Task<string> Editar(Equipamiento objeto)
        {
            string respuesta = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_editarEquipamiento", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdEquipamiento", objeto.IdEquipamiento);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@MsgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al editar equipamiento";
                }
            }

            return respuesta;
        }

        public async Task<int> Eliminar(int id)
        {
            int respuesta = 1;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_eliminarEquipamiento", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdEquipamiento", id);

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

        public async Task<List<Equipamiento>> ListarPorEspacio(int idEspacio)
        {
            var lista = new List<Equipamiento>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEquipamientoPorEspacio", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdEspacio", idEspacio);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Equipamiento
                        {
                            IdEquipamiento = Convert.ToInt32(dr["IdEquipamiento"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Descripcion = dr["Descripcion"].ToString()!,
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                        });
                    }
                }
            }

            return lista;
        }
    }
}
