namespace SigesaEntidades
{
    public class RolUsuario
    {
        public int IdRolUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
