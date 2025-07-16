using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.DB
{
    public class TipoEspacioRepositorio : ITipoEspacioRepositorio
    {
        private readonly SigesaDbContext _context;

        public TipoEspacioRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoEspacio>> ObtenerListaAsync()
        {
            return await _context.TipoEspacios.ToListAsync();
        }

        public async Task<TipoEspacio?> ObtenerPorIdAsync(int id)
        {
            return await _context.TipoEspacios.FindAsync(id);
        }

        public async Task<int> GuardarAsync(TipoEspacio modelo)
        {
            modelo.FechaCreacion = DateTime.Now;
            _context.TipoEspacios.Add(modelo);
            await _context.SaveChangesAsync();
            return modelo.IdTipoEspacio;
        }

        public async Task<bool> EditarAsync(TipoEspacio modelo)
        {
            var existente = await _context.TipoEspacios.FindAsync(modelo.IdTipoEspacio);
            if (existente == null) return false;

            existente.Nombre = modelo.Nombre;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existente = await _context.TipoEspacios.FindAsync(id);
            if (existente == null) return false;

            _context.TipoEspacios.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
