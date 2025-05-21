

using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.Mock
{
    public class MockEquipamientoRepositorio : IEquipamientoRepositorio
    {
        public Task<string> Editar(Equipamiento objeto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Guardar(Equipamiento objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Equipamiento>> Lista()
        {
            throw new NotImplementedException();
        }
    }
}
