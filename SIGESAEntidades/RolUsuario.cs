

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class RolUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRolUsuario { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        // Relacion inversa con la entidad Rol
        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();
    }


}
