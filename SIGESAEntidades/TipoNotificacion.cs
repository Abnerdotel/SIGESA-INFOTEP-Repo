

namespace SigesaEntidades
{
    public class TipoNotificacion
    {
        public int IdTipoNotificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }

}
