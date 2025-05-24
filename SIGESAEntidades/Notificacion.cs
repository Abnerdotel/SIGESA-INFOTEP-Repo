

namespace SigesaEntidades
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public TipoNotificacion Tipo { get; set; } = null!; // Recordatorio, Cancelación
        public DateTime FechaEnvio { get; set; } 
    }
}
