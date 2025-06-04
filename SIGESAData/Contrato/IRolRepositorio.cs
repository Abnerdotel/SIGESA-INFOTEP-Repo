
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IRolRepositorio
    {
        Task<List<Rol>> Lista();
        Task<Rol?> ObtenerPorId(int id);
        Task<string> Guardar(Rol objeto);
        Task<string> Editar(Rol objeto);
        Task<int> Eliminar(int id);

    }
}
