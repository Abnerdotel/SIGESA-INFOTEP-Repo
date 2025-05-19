

using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.Mock
{
    public class MockRolUsario : IRolUsuarioRepositorio
    {
        public Task<List<RolUsuario>> Lista()
        {
            throw new NotImplementedException();
        }
    }
}
