
    using SigesaEntidades;

    namespace SigesaData.Contrato
    {
        public interface ITipoEspacioRepositorio
        {
            Task<IEnumerable<TipoEspacio>> ObtenerListaAsync();
            Task<TipoEspacio?> ObtenerPorIdAsync(int id);
            Task<int> GuardarAsync(TipoEspacio modelo);
            Task<bool> EditarAsync(TipoEspacio modelo);
            Task<bool> EliminarAsync(int id);
        }
    }



