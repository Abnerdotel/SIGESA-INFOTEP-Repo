

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<Usuario>> ObtenerListaAsync(int idRolUsuario = 0);
        Task<Usuario?> AutenticarAsync(string documentoIdentidad, string clave);
        Task<int> GuardarAsync(Usuario usuario);
        Task<bool> EditarAsync(Usuario usuario);
        Task<bool> EliminarAsync(int id);
    }

}
