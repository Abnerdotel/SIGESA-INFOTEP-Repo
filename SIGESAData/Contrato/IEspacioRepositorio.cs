

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEspacioRepositorio
    {
        Task<IEnumerable<Espacio>> ObtenerListaAsync();
        Task<Espacio?> ObtenerPorIdAsync(int id);
        Task<int> GuardarAsync(Espacio espacio);
        Task<bool> EditarAsync(Espacio espacio);
        Task<bool> EliminarAsync(int id);
        Task<IEnumerable<Espacio>> ObtenerPorTipoAsync(int idTipo);
    }

}
