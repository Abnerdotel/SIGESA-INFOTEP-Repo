
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Rol
    {
        public int IdRol { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public int IdRolUsuario { get; set; }
        public virtual RolUsuario RolUsuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }

}
