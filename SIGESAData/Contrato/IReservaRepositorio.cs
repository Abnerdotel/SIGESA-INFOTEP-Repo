
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IReservaRepositorio
    {
        Task<List<Reserva>> Lista();
        Task<Reserva?> ObtenerPorId(int id);
        Task<string> Guardar(Reserva objeto);
        Task<string> Editar(Reserva objeto);
        Task<int> Eliminar(int id);
        Task<List<Reserva>> ListarPorUsuario(int idUsuario);
        Task<List<Reserva>> ListarPorEspacio(int idEspacio);

    }
}
