using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class NotificacionRepositorio : INotificacionRepositorio
    {
        private readonly SigesaDbContext _context;

        public NotificacionRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegistrarAsync(Notificacion notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();
            return notificacion.IdNotificacion;
        }

        public async Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _context.Notificaciones
                .Include(n => n.Tipo)
                .Where(n => n.IdUsuario == idUsuario)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync();
        }

        public async Task<bool> MarcarComoLeidaAsync(int idNotificacion)
        {
            var notificacion = await _context.Notificaciones.FindAsync(idNotificacion);
            if (notificacion == null)
                return false;

            notificacion.Leido = true;
            _context.Notificaciones.Update(notificacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarcarTodasComoLeidasAsync(int idUsuario)
        {
            var notificaciones = await _context.Notificaciones
                .Where(n => n.IdUsuario == idUsuario && !n.Leido)
                .ToListAsync();

            if (!notificaciones.Any()) return false;

            foreach (var n in notificaciones)
            {
                n.Leido = true;
            }

            _context.Notificaciones.UpdateRange(notificaciones);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
