
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IReservaRepositorio
    {
        Task<IEnumerable<Reserva>> ObtenerListaAsync();
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task<int> GuardarAsync(Reserva reserva);
        Task<bool> EditarAsync(Reserva reserva);
        Task<bool> EliminarAsync(int id);
        Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Reserva>> ObtenerPorEspacioAsync(int idEspacio);
    }

}
