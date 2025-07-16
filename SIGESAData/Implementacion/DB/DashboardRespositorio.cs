using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;

namespace SigesaData.Implementacion.DB
{
    public class DashboardRespositorio : IDashboardRespositorio
    {
        private readonly SigesaDbContext _context;

        public DashboardRespositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<int> ObtenerTotalReservasAsync()
        {
            return await _context.Reservas.CountAsync();
        }

        public async Task<int> ObtenerEspaciosActivosAsync()
        {
            return await _context.Espacios.CountAsync();
        }

        public async Task<int> ObtenerReservasCanceladasAsync()
        {
            return await _context.Reservas
                .Where(r => r.Estado.Nombre == "Cancelada")
                .CountAsync();
        }

        public async Task<int> ObtenerIncidenciasReportadasAsync()
        {
            return await _context.Bitacoras
                .Where(b => b.Modulo == "Incidencia")
                .CountAsync();
        }
    }
}
