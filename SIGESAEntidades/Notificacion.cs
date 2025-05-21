

namespace SigesaEntidades
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public string Mensaje { get; set; } = null!;
        public string Tipo { get; set; } = null!; // Ej: Recordatorio, Cancelación
        public string FechaEnvio { get; set; } = null!;
    }
}
