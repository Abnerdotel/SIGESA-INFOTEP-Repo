namespace SigesaWeb.Models.DTOS
{
    using System.ComponentModel.DataAnnotations;

    public class VMUsuario
    {
        [Required]
        public string DocumentoIdentidad { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Clave), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; } = string.Empty;
    }

}
