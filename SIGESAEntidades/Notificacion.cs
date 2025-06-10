

namespace SigesaEntidades
{
    public class Notificacion
    {        
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }        
        public virtual Usuario Usuario { get; set; } = null!;       
        public string Mensaje { get; set; } = null!;
        public int IdTipoNotificacion { get; set; }    
        public virtual TipoNotificacion Tipo { get; set; } = null!;       
        public DateTime FechaEnvio { get; set; }
    }

}
