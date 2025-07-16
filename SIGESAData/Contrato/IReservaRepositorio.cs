
using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IReservaRepositorio
    {
        // CRUD Básico
        Task<IEnumerable<Reserva>> ObtenerListaAsync();
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task<int> GuardarAsync(Reserva reserva);
        Task<bool> EditarAsync(Reserva reserva);
        Task<bool> EliminarAsync(int id);

        // Filtrado avanzado
        Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Reserva>> ObtenerPorEspacioAsync(int idEspacio);
        Task<IEnumerable<Reserva>> ObtenerPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);

        // Cambiar estado (pendiente, aprobada, cancelada, rechazada...)
        Task<bool> CambiarEstadoAsync(int idReserva, int idNuevoEstado);

        // Historial y Estado actual
        Task<IEnumerable<Reserva>> ObtenerHistorialUsuarioAsync(int idUsuario);
        Task<IEnumerable<Reserva>> ObtenerPendientesPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Reserva>> ObtenerConfirmadasAsync();
        Task<IEnumerable<Reserva>> ObtenerCanceladasAsync();

        // Verificar disponibilidad (reservas que coincidan con espacio/fecha/hora)
        Task<bool> VerificarDisponibilidadAsync(int idEspacio, DateTime fechaInicio, DateTime fechaFin);
    }
}
