

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface INotificacionRepositorio
    {
        public interface INotificacionRepositorio
        {
            Task<string> Registrar(Notificacion objeto);
            Task<List<Notificacion>> ListarPorUsuario(int idUsuario);

        }
    }
}
