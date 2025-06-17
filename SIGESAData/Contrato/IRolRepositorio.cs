
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IRolRepositorio
    {
        Task<IEnumerable<Rol>> ObtenerListaAsync();
        Task<Rol?> ObtenerPorIdAsync(int id);
        Task<int> GuardarAsync(Rol rol);
        Task<bool> EliminarAsync(int id);
        Task<IEnumerable<Rol>> ObtenerPorUsuarioAsync(int idUsuario); 
    }


}
