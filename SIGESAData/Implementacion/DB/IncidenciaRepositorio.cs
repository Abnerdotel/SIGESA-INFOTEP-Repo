using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class IncidenciaRepositorio : IIncidenciaRepositorio
    {
        private readonly SigesaDbContext _context;

        public IncidenciaRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bitacora>> ObtenerIncidenciasPorEspacioAsync(int idEspacio)
        {
            return await _context.Bitacoras
                .Where(b => b.Modulo == "Incidencia" && b.Detalle.Contains($"Espacio ID {idEspacio}"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Bitacora>> ObtenerIncidenciasPorUsuarioAsync(int idUsuario)
        {
            return await _context.Bitacoras
                .Where(b => b.Modulo == "Incidencia" && b.IdUsuario == idUsuario)
                .ToListAsync();
        }

        public async Task<int> ReportarIncidenciaAsync(Bitacora incidencia)
        {
            incidencia.FechaAccion = DateTime.Now;
            _context.Bitacoras.Add(incidencia);
            await _context.SaveChangesAsync();
            return incidencia.IdBitacora;
        }

        public async Task<bool> ResolverIncidenciaAsync(int idIncidencia)
        {
            var incidencia = await _context.Bitacoras.FindAsync(idIncidencia);
            if (incidencia == null) return false;

            incidencia.Detalle += " (Resuelta)";
            _context.Bitacoras.Update(incidencia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}