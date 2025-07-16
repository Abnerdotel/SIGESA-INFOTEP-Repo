using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEquipoRepositorio
    {
        Task<IEnumerable<Equipamiento>> ObtenerEquiposAsignadosAsync(int idCoordinador);
        Task<bool> ReportarFallaAsync(int idEquipo, string descripcion, int idUsuario);
    }
}
