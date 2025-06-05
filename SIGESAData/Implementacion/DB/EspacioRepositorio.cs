
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class EspacioRepositorio : IEspacioRepositorio
    {
        private readonly SigesaDbContext _context;

        public EspacioRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Espacio>> ObtenerListaAsync()
        {
            return await _context.Espacios
                .Include(e => e.Tipo)
                .ToListAsync();
        }

        public async Task<Espacio?> ObtenerPorIdAsync(int id)
        {
            return await _context.Espacios
                .Include(e => e.Tipo)
                .Include(e => e.Equipamientos)
                .ThenInclude(ee => ee.Equipamiento)
                .FirstOrDefaultAsync(e => e.IdEspacio == id);
        }

        public async Task<int> GuardarAsync(Espacio espacio)
        {
            _context.Espacios.Add(espacio);
            await _context.SaveChangesAsync();
            return espacio.IdEspacio;
        }

        public async Task<bool> EditarAsync(Espacio espacio)
        {
            var existente = await _context.Espacios.FindAsync(espacio.IdEspacio);
            if (existente == null) return false;

            _context.Entry(existente).CurrentValues.SetValues(espacio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var espacio = await _context.Espacios.FindAsync(id);
            if (espacio == null) return false;

            _context.Espacios.Remove(espacio);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Espacio>> ObtenerPorTipoAsync(int idTipo)
        {
            return await _context.Espacios
                .Where(e => e.IdTipoEspacio == idTipo)
                .ToListAsync();
        }
    }
}