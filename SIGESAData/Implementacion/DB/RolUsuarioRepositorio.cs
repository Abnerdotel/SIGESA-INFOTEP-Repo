﻿
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class RolUsuarioRepositorio : IRolUsuarioRepositorio
    {
        private readonly ConnectionStrings con;
        public RolUsuarioRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<List<RolUsuario>> Lista()
        {
            List<RolUsuario> lista = new List<RolUsuario>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaRolUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new RolUsuario()
                        {
                            IdRolUsuario = Convert.ToInt32(dr["IdRolUsuario"]),
                            Nombre = dr["Nombre"].ToString()!,
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}
