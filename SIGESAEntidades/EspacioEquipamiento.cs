
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigesaEntidades
{
    public class EspacioEquipamiento
    {
        
        public int IdEspacioEquipamiento { get; set; }
        public int IdEspacio { get; set; }
        public virtual Espacio Espacio { get; set; } = null!;
        public int IdEquipamiento { get; set; }       
        public virtual Equipamiento Equipamiento { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }

}
