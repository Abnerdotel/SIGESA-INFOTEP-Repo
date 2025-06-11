using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{

    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }
        public string NumeroDocumentoIdentidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
    }


}
