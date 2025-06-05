using Microsoft.EntityFrameworkCore;
using SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;


namespace SigesaData.Implementacion.DB
{
    public class RolUsuarioRepositorio : IRolUsuarioRepositorio
    {
        private readonly SigesaDbContext _context;

        public RolUsuarioRepositorio(SigesaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolUsuario>> ObtenerListaAsync()
        {
            return await _context.RolUsuarios
                .Include(r => r.Roles)
                .ToListAsync();
        }
    }
}

