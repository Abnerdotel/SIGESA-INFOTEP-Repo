

namespace SigesaEntidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }

        public int IdEspacio { get; set; }
        public Espacio Espacio { get; set; } = null!;

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public string FechaInicio { get; set; } = null!;
        public string FechaFin { get; set; } = null!;
        public string Estado { get; set; } = null!; // Ej: Pendiente, Confirmada, Cancelada

        public string FechaCreacion { get; set; } = null!;
    }
}
