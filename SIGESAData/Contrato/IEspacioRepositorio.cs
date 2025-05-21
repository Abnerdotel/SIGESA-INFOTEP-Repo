

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IEspacioRepositorio
    {
        Task<List<Espacio>> Lista();
        Task<string> Guardar(Espacio objeto);
        Task<string> Editar(Espacio objeto);
        Task<int> Eliminar(int Id);
    }
}
