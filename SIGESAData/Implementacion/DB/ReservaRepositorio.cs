using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class ReservaRepositorio : IReservaRepositorio
    {
        private readonly ConnectionStrings con;

        public ReservaRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<List<Reserva>> Lista()
        {
            var lista = new List<Reserva>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaReserva", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Reserva
                        {
                            IdReserva = Convert.ToInt32(dr["IdReserva"]),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                            FechaCreacion = dr["FechaCreacion"].ToString()!,
                            Estado = new EstadoReserva
                            {
                                IdEstado = Convert.ToInt32(dr["IdEstado"]),
                                Nombre = dr["NombreEstado"].ToString()!
                            },
                            Usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["NombreUsuario"].ToString()!
                            },
                            Espacio = new Espacio
                            {
                                IdEspacio = Convert.ToInt32(dr["IdEspacio"]),
                                Nombre = dr["NombreEspacio"].ToString()!
                            }
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<Reserva?> ObtenerPorId(int id)
        {
            Reserva? reserva = null;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerReservaPorId", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdReserva", id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        reserva = new Reserva
                        {
                            IdReserva = Convert.ToInt32(dr["IdReserva"]),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                            FechaCreacion = dr["FechaCreacion"].ToString()!,
                            Estado = new EstadoReserva
                            {
                                IdEstado = Convert.ToInt32(dr["IdEstado"]),
                                Nombre = dr["NombreEstado"].ToString()!
                            },
                            Usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["NombreUsuario"].ToString()!
                            },
                            Espacio = new Espacio
                            {
                                IdEspacio = Convert.ToInt32(dr["IdEspacio"]),
                                Nombre = dr["NombreEspacio"].ToString()!
                            }
                        };
                    }
                }
            }

            return reserva;
        }

        public async Task<string> Guardar(Reserva objeto)
        {
            string respuesta = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_guardarReserva", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdEspacio", objeto.Espacio.IdEspacio);
                cmd.Parameters.AddWithValue("@IdUsuario", objeto.Usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@FechaInicio", objeto.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", objeto.FechaFin);
                cmd.Parameters.AddWithValue("@IdEstado", objeto.Estado.IdEstado);

                var output = new SqlParameter("@MsgError", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(output);

                await cmd.ExecuteNonQueryAsync();
                respuesta = output.Value.ToString()!;
            }

            return respuesta;
        }

        public async Task<string> Editar(Reserva objeto)
        {
            string respuesta = "";

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_editarReserva", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@IdReserva", objeto.IdReserva);
                cmd.Parameters.AddWithValue("@IdEspacio", objeto.Espacio.IdEspacio);
                cmd.Parameters.AddWithValue("@IdUsuario", objeto.Usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@FechaInicio", objeto.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", objeto.FechaFin);
                cmd.Parameters.AddWithValue("@IdEstado", objeto.Estado.IdEstado);

                var output = new SqlParameter("@MsgError", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(output);

                await cmd.ExecuteNonQueryAsync();
                respuesta = output.Value.ToString()!;
            }

            return respuesta;
        }

        public async Task<int> Eliminar(int id)
        {
            int resultado = 0;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_eliminarReserva", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdReserva", id);

                resultado = await cmd.ExecuteNonQueryAsync();
            }

            return resultado;
        }

        public async Task<List<Reserva>> ListarPorUsuario(int idUsuario)
        {
            var lista = new List<Reserva>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listarReservaPorUsuario", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Reserva
                        {
                            IdReserva = Convert.ToInt32(dr["IdReserva"]),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                            FechaCreacion = dr["FechaCreacion"].ToString()!,
                            Estado = new EstadoReserva
                            {
                                IdEstado = Convert.ToInt32(dr["IdEstado"]),
                                Nombre = dr["NombreEstado"].ToString()!
                            },
                            Espacio = new Espacio
                            {
                                IdEspacio = Convert.ToInt32(dr["IdEspacio"]),
                                Nombre = dr["NombreEspacio"].ToString()!
                            }
                        });
                    }
                }
            }

            return lista;
        }

        public async Task<List<Reserva>> ListarPorEspacio(int idEspacio)
        {
            var lista = new List<Reserva>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listarReservaPorEspacio", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@IdEspacio", idEspacio);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Reserva
                        {
                            IdReserva = Convert.ToInt32(dr["IdReserva"]),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                            FechaCreacion = dr["FechaCreacion"].ToString()!,
                            Estado = new EstadoReserva
                            {
                                IdEstado = Convert.ToInt32(dr["IdEstado"]),
                                Nombre = dr["NombreEstado"].ToString()!
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
