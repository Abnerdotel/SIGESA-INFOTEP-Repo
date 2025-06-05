

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface INotificacionRepositorio
    {
        Task<int> RegistrarAsync(Notificacion notificacion);
        Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int idUsuario);
    }

}
