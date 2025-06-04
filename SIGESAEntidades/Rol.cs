

namespace SigesaEntidades
{
    public class Rol
    {
        public int IdRol { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual RolUsuario RolUsuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
}
