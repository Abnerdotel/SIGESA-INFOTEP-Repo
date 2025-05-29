

namespace SigesaEntidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public virtual Espacio Espacio { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public virtual EstadoReserva Estado { get; set; } = null!; // Pendiente, Confirmada, Cancelada

        public string FechaCreacion { get; set; } = null!;
    }
}
