using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface ISalonRepositorio
    {
        Task<IEnumerable<Espacio>> ObtenerSalonesAsignadosAsync(int idCoordinador);
    }
}
