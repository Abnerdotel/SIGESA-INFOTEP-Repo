namespace SigesaWeb.Models.DTOs
{
    public class UsuarioCreateDTO
    {
        public string NumeroDocumentoIdentidad { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public int IdRolUsuario { get; set; }
    }

}
