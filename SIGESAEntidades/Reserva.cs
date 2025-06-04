

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Reserva
    {
      
        public int IdReserva { get; set; }
        public int IdEspacio { get; set; }       
        public virtual Espacio Espacio { get; set; } = null!;
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;      
        public DateTime FechaInicio { get; set; }       
        public DateTime FechaFin { get; set; }
        public int IdEstado { get; set; }       
        public virtual EstadoReserva Estado { get; set; } = null!; 
        public DateTime FechaCreacion { get; set; }
    }

}
