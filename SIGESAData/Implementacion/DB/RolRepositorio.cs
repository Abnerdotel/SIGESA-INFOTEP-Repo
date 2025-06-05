using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly SigesaDbContext _context;

        public RolRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> ObtenerListaAsync()
        {
            return await _context.Roles
                .Include(r => r.Usuario)
                .Include(r => r.RolUsuario)
                .ToListAsync();
        }

        public async Task<Rol?> ObtenerPorIdAsync(int id)
        {
            return await _context.Roles
                .Include(r => r.Usuario)
                .Include(r => r.RolUsuario)
                .FirstOrDefaultAsync(r => r.IdRol == id);
        }

        public async Task<int> GuardarAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol.IdRol;
        }

        public async Task<bool> EditarAsync(Rol rol)
        {
            var existente = await _context.Roles.FindAsync(rol.IdRol);
            if (existente == null) return false;

            _context.Entry(existente).CurrentValues.SetValues(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

