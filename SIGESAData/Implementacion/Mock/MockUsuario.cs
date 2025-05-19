
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaData.Implementacion.Mock
{
    public class MockUsuario : IUsuarioRepositorio
    {
        public Task<string> Editar(Usuario Objeto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Guardar(Usuario objeto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> Lista(int IdRolUsuario = 0)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> Login(string DocumentoIdentidad, string Clave)
        {
            throw new NotImplementedException();
        }
    }
}
