namespace SigesaData.Contrato
{

    public interface IReporteRepositorio
    {
        Task<byte[]> GenerarReporteReservasAsync(DateTime inicio, DateTime fin);
        Task<byte[]> GenerarReporteUsuariosAsync();
        Task<byte[]> GenerarReporteEspaciosAsync();


    }
}
