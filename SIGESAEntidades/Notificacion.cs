

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    
    public class Notificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNotificacion { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; } = null!;

        [Required]
        public string Mensaje { get; set; } = null!;

        public int IdTipoNotificacion { get; set; }
        [ForeignKey("IdTipoNotificacion")]
        public virtual TipoNotificacion Tipo { get; set; } = null!;

        public DateTime FechaEnvio { get; set; }

        public bool Leido { get; set; } = false;
    }


}
