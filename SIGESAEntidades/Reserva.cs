

namespace SigesaEntidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public Espacio Espacio { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoReserva Estado { get; set; } = null!; // Pendiente, Confirmada, Cancelada

        public string FechaCreacion { get; set; } = null!;
    }
}
