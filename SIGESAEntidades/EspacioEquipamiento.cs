

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class EspacioEquipamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEspacioEquipamiento { get; set; }
        public int IdEspacio { get; set; }

        [ForeignKey("IdEspacio")]
        public virtual Espacio Espacio { get; set; } = null!;
        public int IdEquipamiento { get; set; }

        [ForeignKey("IdEquipamiento")]
        public virtual Equipamiento Equipamiento { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }

}
