namespace SigesaData.Contrato
{
    public interface IDashboardRespositorio
    {
        Task<int> ObtenerTotalReservasAsync();
        Task<int> ObtenerEspaciosActivosAsync();
        Task<int> ObtenerReservasCanceladasAsync();
        Task<int> ObtenerIncidenciasReportadasAsync();
    }
}
