

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; } = null!;

        [Required]
        public int IdRolUsuario { get; set; }

        [ForeignKey(nameof(IdRolUsuario))]
        public virtual RolUsuario RolUsuario { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }
    }

}
