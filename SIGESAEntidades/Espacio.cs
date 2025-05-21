

namespace SigesaEntidades
{
    public class Espacio
    {
        public int IdEspacio { get; set; }
        public string Nombre { get; set; } = null!;
        public int Capacidad { get; set; }
        public string Tipo { get; set; } = null!; // Ej: Aula, Sala de Reunión
        public string? Observaciones { get; set; }
        public string FechaCreacion { get; set; } = null!;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<EspacioEquipamiento> Equipamientos { get; set; } = new List<EspacioEquipamiento>();
    }
}
