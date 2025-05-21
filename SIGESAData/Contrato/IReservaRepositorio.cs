
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IReservaRepositorio
    {
        Task<List<Reserva>> Lista();
        Task<string> Guardar(Reserva objeto);
        Task<string> Editar(Reserva objeto);
        Task<int> Eliminar(int Id);
    }
}
