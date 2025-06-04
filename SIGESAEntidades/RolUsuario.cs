namespace SigesaEntidades
{
    public class RolUsuario
    {
        public int IdRolUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();
    }

}
