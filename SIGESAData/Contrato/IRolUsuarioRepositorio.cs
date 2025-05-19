
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IRolUsuarioRepositorio
    {
        Task<List<RolUsuario>> Lista();
    }
}
