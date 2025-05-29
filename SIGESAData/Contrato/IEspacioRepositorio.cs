

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEspacioRepositorio
    {
        Task<List<Espacio>> Lista();
        Task<Espacio?> ObtenerPorId(int id);
        Task<string> Guardar(Espacio objeto);
        Task<string> Editar(Espacio objeto);
        Task<int> Eliminar(int id);
        Task<List<Espacio>> ListarPorTipo(int idTipo);
    }
}
