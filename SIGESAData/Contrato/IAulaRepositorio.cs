using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IAulaRepositorio
    {
        Task<IEnumerable<Espacio>> ObtenerAulasAsignadasAsync(int idCoordinador);
        Task<bool> RegistrarDisponibilidadAsync(int idEspacio, DateTime fechaInicio, DateTime fechaFin);
    }
}
