

using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.Mock
{
    public class MockEspacioRepositorio : IEspacioRepositorio
    {
        public Task<string> Editar(Espacio objeto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Guardar(Espacio objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Espacio>> Lista()
        {
            throw new NotImplementedException();
        }
    }
}
