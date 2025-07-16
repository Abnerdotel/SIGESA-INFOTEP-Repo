using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class SalonRepositorio : ISalonRepositorio
    {
        private readonly SigesaDbContext _context;

        public SalonRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Espacio>> ObtenerSalonesAsignadosAsync(int idCoordinador)
        {
            return await _context.Espacios
                .Where(e => e.Tipo.Nombre.Contains("Salón"))
                .ToListAsync();
        }
    }
}