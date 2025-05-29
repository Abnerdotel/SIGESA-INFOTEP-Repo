
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class EspacioEquipamiento
    { 
        int IdEspacioEquipamiento {  get; set; }
        public virtual Espacio Espacio { get; set; } = null!;
        public virtual Equipamiento Equipamiento { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
}
