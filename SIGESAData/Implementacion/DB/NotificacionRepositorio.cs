using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

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
    }
}
