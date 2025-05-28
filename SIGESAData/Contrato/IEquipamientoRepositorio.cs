

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEquipamientoRepositorio
    {
        Task<List<Equipamiento>> Lista();
        Task<Equipamiento?> ObtenerPorId(int id);
        Task<string> Guardar(Equipamiento objeto);
        Task<string> Editar(Equipamiento objeto);
        Task<int> Eliminar(int id);

        Task<List<Equipamiento>> ListarPorEspacio(int idEspacio);
    }
}
