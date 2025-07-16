using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IIncidenciaRepositorio
    {
        Task<IEnumerable<Bitacora>> ObtenerIncidenciasPorEspacioAsync(int idEspacio);
        Task<IEnumerable<Bitacora>> ObtenerIncidenciasPorUsuarioAsync(int idUsuario);
        Task<int> ReportarIncidenciaAsync(Bitacora incidencia);
        Task<bool> ResolverIncidenciaAsync(int idIncidencia);
    }
}
