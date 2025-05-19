namespace SigesaWeb.Models.DTOs
{
    public class VMUsuario
    {
        public string DocumentoIdentidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string ConfirmarClave { get; set; } = null!;
    }
}
