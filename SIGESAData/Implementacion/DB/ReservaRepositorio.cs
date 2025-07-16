using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class ReservaRepositorio : IReservaRepositorio
    {
        private readonly SigesaDbContext _context;

        public ReservaRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> ObtenerListaAsync()
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Espacio)
                .Include(r => r.Estado)
                .ToListAsync();
        }

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Espacio)
                .Include(r => r.Estado)
                .FirstOrDefaultAsync(r => r.IdReserva == id);
        }

        public async Task<int> GuardarAsync(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return reserva.IdReserva;
        }

        public async Task<bool> EditarAsync(Reserva reserva)
        {
            var existente = await _context.Reservas.FindAsync(reserva.IdReserva);
            if (existente == null) return false;

            _context.Entry(existente).CurrentValues.SetValues(reserva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _context.Reservas
                .Where(r => r.IdUsuario == idUsuario)
                .Include(r => r.Espacio)
                .Include(r => r.Estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorEspacioAsync(int idEspacio)
        {
            return await _context.Reservas
                .Where(r => r.IdEspacio == idEspacio)
                .Include(r => r.Usuario)
                .Include(r => r.Estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Reservas
                .Where(r => r.FechaInicio >= fechaInicio && r.FechaFin <= fechaFin)
                .Include(r => r.Espacio)
                .Include(r => r.Usuario)
                .Include(r => r.Estado)
                .ToListAsync();
        }

        public async Task<bool> CambiarEstadoAsync(int idReserva, int idNuevoEstado)
        {
            var reserva = await _context.Reservas.FindAsync(idReserva);
            if (reserva == null) return false;

            reserva.IdEstado = idNuevoEstado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reserva>> ObtenerHistorialUsuarioAsync(int idUsuario)
        {
            return await _context.Reservas
                .Where(r => r.IdUsuario == idUsuario)
                .OrderByDescending(r => r.FechaInicio)
                .Include(r => r.Espacio)
                .Include(r => r.Estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerPendientesPorUsuarioAsync(int idUsuario)
        {
            return await _context.Reservas
                .Where(r => r.IdUsuario == idUsuario && r.Estado.Nombre == "Pendiente")
                .Include(r => r.Espacio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerConfirmadasAsync()
        {
            return await _context.Reservas
                .Where(r => r.Estado.Nombre == "Confirmada")
                .Include(r => r.Usuario)
                .Include(r => r.Espacio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ObtenerCanceladasAsync()
        {
            return await _context.Reservas
                .Where(r => r.Estado.Nombre == "Cancelada")
                .Include(r => r.Usuario)
                .Include(r => r.Espacio)
                .ToListAsync();
        }

        public async Task<bool> VerificarDisponibilidadAsync(int idEspacio, DateTime fechaInicio, DateTime fechaFin)
        {
            return !await _context.Reservas.AnyAsync(r =>
                r.IdEspacio == idEspacio &&
                r.FechaInicio < fechaFin &&
                r.FechaFin > fechaInicio);
        }
    }
}
