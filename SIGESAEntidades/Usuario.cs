namespace SigesaEntidades
{

        public class Usuario
        {
        public int IdUsuario { get; set; }
        public string NumeroDocumentoIdentidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();
    }

}
