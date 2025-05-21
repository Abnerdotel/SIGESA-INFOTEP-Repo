

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEquipamientoRepositorio
    {
        Task<List<Equipamiento>> Lista();
        Task<string> Guardar(Equipamiento objeto);
        Task<string> Editar(Equipamiento objeto);
        Task<int> Eliminar(int Id);
    }
}
