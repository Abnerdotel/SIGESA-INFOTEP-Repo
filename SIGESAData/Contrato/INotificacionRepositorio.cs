using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface INotificacionRepositorio
    {
        Task<int> RegistrarAsync(Notificacion notificacion);
        Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<bool> MarcarComoLeidaAsync(int idNotificacion);
        Task<bool> MarcarTodasComoLeidasAsync(int idUsuario);
    }
}
