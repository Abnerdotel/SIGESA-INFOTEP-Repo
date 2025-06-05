

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEquipamientoRepositorio
    {
        Task<IEnumerable<Equipamiento>> ObtenerListaAsync();
        Task<Equipamiento?> ObtenerPorIdAsync(int id);
        Task<int> GuardarAsync(Equipamiento equipamiento);
        Task<bool> EditarAsync(Equipamiento equipamiento);
        Task<bool> EliminarAsync(int id);
        Task<IEnumerable<Equipamiento>> ObtenerPorEspacioAsync(int idEspacio);
    }

}
