
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IRolUsuarioRepositorio
    {
        Task<IEnumerable<RolUsuario>> ObtenerListaAsync();
    }
}
