using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class BitacoraRepositorio : IBitacoraRepositorio
    {
        private readonly SigesaDbContext _context;

        public BitacoraRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegistrarAsync(Bitacora bitacora)
        {
            _context.Bitacoras.Add(bitacora);
            await _context.SaveChangesAsync();
            return bitacora.IdBitacora;
        }

        public async Task<IEnumerable<Bitacora>> ObtenerListaAsync(string? modulo = null, string? usuario = null, DateTime? desde = null, DateTime? hasta = null)
        {
            var query = _context.Bitacoras.AsQueryable();

            if (!string.IsNullOrEmpty(modulo))
                query = query.Where(b => b.Modulo.ToLower().Contains(modulo.ToLower()));

            if (!string.IsNullOrEmpty(usuario))
                query = query.Where(b => b.Usuario.Nombre.ToLower().Contains(usuario.ToLower()));

            if (desde.HasValue)
                query = query.Where(b => b.FechaAccion >= desde.Value);

            if (hasta.HasValue)
                query = query.Where(b => b.FechaAccion <= hasta.Value);

            return await query
                .Include(b => b.Usuario)
                .OrderByDescending(b => b.FechaAccion)
                .ToListAsync();
        }

        public async Task<Bitacora?> ObtenerPorIdAsync(int id)
        {
            return await _context.Bitacoras
                .Include(b => b.Usuario)
                .FirstOrDefaultAsync(b => b.IdBitacora == id);
        }
    }
}
