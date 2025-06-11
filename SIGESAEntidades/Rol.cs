

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; } = null!;
        public int IdRolUsuario { get; set; }
        [ForeignKey("IdRolUsuario")]
        public virtual RolUsuario RolUsuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }

}
