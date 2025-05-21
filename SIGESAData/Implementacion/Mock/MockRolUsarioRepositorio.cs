

using SigesaData.Context;
using SigesaData.Contrato;
using SigesaEntidades;
using Microsoft.EntityFrameworkCore;

namespace SigesaData.Implementacion.Mock
{
    public class MockRolUsarioRepositorio : IRolUsuarioRepositorio
    {
        private readonly RolUsuarioContext _context;

        public MockRolUsarioRepositorio(RolUsuarioContext context)
        {
            _context = context;
        }

        public async Task<List<RolUsuario>> Lista()
        {
            return await _context.RolesUsuario.ToListAsync();
        }
    }
}
