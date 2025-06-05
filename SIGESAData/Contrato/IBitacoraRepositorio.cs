

using SigesaEntidades;

namespace SigesaData.Contrato
{
  
        public interface IBitacoraRepositorio
        {
            Task<int> RegistrarAsync(Bitacora bitacora);
            Task<IEnumerable<Bitacora>> ObtenerListaAsync(string? modulo = null, string? usuario = null, DateTime? desde = null, DateTime? hasta = null);
            Task<Bitacora?> ObtenerPorIdAsync(int id);
        }



}
