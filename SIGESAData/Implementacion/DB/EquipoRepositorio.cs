using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class EquipoRepositorio : IEquipoRepositorio
    {
        private readonly SigesaDbContext _context;

        public EquipoRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipamiento>> ObtenerEquiposAsignadosAsync(int idCoordinador)
        {
            return await _context.Equipamientos.ToListAsync();
        }

        public async Task<bool> ReportarFallaAsync(int idEquipo, string descripcion, int idUsuario)
        {
            var bitacora = new Bitacora
            {
                IdUsuario = idUsuario,
                Modulo = "Equipos",
                Accion = "Reporte de Falla",
                Detalle = $"Equipo ID {idEquipo}: {descripcion}",
                FechaAccion = DateTime.Now
            };
            _context.Bitacoras.Add(bitacora);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}