using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Context.SigesaData.Context;
using SigesaData.Contrato;
using SigesaData.Utilidades;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SigesaDbContext _context;

        public UsuarioRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerListaAsync(int idRolUsuario = 0)
        {
            var query = _context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(r => r.RolUsuario)
                .AsQueryable();

            if (idRolUsuario > 0)
            {
                query = query.Where(u => u.Roles.Any(r => r.IdRolUsuario == idRolUsuario));
            }

            return await query.ToListAsync();
        }

        public async Task<Usuario?> AutenticarAsync(string documentoIdentidad, string clave)
        {
            return await _context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(r => r.RolUsuario)
                .FirstOrDefaultAsync(u => u.NumeroDocumentoIdentidad == documentoIdentidad && u.Clave == clave);
        }

        public async Task<int> GuardarAsync(Usuario usuario)
        {

            usuario.Clave= Encriptador.HashearClave(usuario.Clave);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario.IdUsuario;
        }

        public async Task<bool> EditarAsync(Usuario usuario)
        {
            var existente = await _context.Usuarios.FindAsync(usuario.IdUsuario);
            if (existente == null) return false;

            _context.Entry(existente).CurrentValues.SetValues(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

