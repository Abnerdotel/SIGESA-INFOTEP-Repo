

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReserva { get; set; }
        public int IdEspacio { get; set; }
        [ForeignKey("IdEspacio")]
        public virtual Espacio Espacio { get; set; } = null!;
        public int IdUsuario { get; set; }
        [ForeignKey("IdEspacio")]
        public virtual Usuario Usuario { get; set; } = null!;      
        public DateTime FechaInicio { get; set; }       
        public DateTime FechaFin { get; set; }
        public int IdEstado { get; set; }
        [ForeignKey("IdEspacio")]
        public virtual EstadoReserva Estado { get; set; } = null!; 
        public DateTime FechaCreacion { get; set; }
    }

}
