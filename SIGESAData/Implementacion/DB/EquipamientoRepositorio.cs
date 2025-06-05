using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class EquipamientoRepositorio : IEquipamientoRepositorio
    {

        private readonly SigesaDbContext _context;

        public EquipamientoRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Equipamiento>> ObtenerListaAsync()
        {
            return await _context.Equipamientos.ToListAsync();
        }

        public async Task<Equipamiento?> ObtenerPorIdAsync(int id)
        {
            return await _context.Equipamientos
                .Include(e => e.Espacios)
                .ThenInclude(ee => ee.Espacio)
                .FirstOrDefaultAsync(e => e.IdEquipamiento == id);
        }

        public async Task<int> GuardarAsync(Equipamiento equipamiento)
        {
            _context.Equipamientos.Add(equipamiento);
            await _context.SaveChangesAsync();
            return equipamiento.IdEquipamiento;
        }

        public async Task<bool> EditarAsync(Equipamiento equipamiento)
        {
            var existente = await _context.Equipamientos.FindAsync(equipamiento.IdEquipamiento);
            if (existente == null) return false;

            _context.Entry(existente).CurrentValues.SetValues(equipamiento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var equipamiento = await _context.Equipamientos.FindAsync(id);
            if (equipamiento == null) return false;

            _context.Equipamientos.Remove(equipamiento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Equipamiento>> ObtenerPorEspacioAsync(int idEspacio)
        {
            return await _context.EspacioEquipamientos
                .Where(ee => ee.IdEspacio == idEspacio)
                .Include(ee => ee.Equipamiento)
                .Select(ee => ee.Equipamiento)
                .ToListAsync();
        }
    }
}
