using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class AulaRepositorio : IAulaRepositorio
    {
        private readonly SigesaDbContext _context;

        public AulaRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Espacio>> ObtenerAulasAsignadasAsync(int idCoordinador)
        {
            return await _context.Espacios
                .Where(e => e.Tipo.Nombre.Contains("Aula"))
                .ToListAsync();
        }

        public async Task<bool> RegistrarDisponibilidadAsync(int idEspacio, DateTime fechaInicio, DateTime fechaFin)
        {
            var existe = await _context.Reservas.AnyAsync(r => r.IdEspacio == idEspacio && r.FechaInicio < fechaFin && r.FechaFin > fechaInicio);
            return !existe;
        }
    }
}