

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IBitacoraRepositorio
    {
        Task<string> Registrar(Bitacora objeto);
        Task<List<Bitacora>> Lista(string? modulo = null, string? usuario = null, DateTime? desde = null, DateTime? hasta = null);
        Task<Bitacora?> ObtenerPorId(int id);
        Task<int> Eliminar(int id); 
        // opcional si se decide limpiar los logs

    }
}
