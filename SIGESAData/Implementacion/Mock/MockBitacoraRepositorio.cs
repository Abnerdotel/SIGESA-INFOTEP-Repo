

using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.Mock
{
    public class MockBitacoraRepositorio : IBitacoraRepositorio
    {
        public Task<int> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Bitacora>> Lista(string? modulo = null, string? usuario = null, DateTime? desde = null, DateTime? hasta = null)
        {
            throw new NotImplementedException();
        }

        public Task<Bitacora?> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Registrar(Bitacora objeto)
        {
            throw new NotImplementedException();
        }
    }
}
