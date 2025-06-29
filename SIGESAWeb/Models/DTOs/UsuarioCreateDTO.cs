namespace SigesaWeb.Models.DTOs
{
    public class UsuarioCreateDTO
    {
        public string NumeroDocumentoIdentidad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int IdRolUsuario { get; set; }
    }

}
