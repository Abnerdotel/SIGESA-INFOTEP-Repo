


namespace SigesaEntidades
{
    public class EstadoReserva
    {
        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}
