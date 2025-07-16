namespace SigesaWeb.Models.DTOs
{
    using SigesaWeb.Models.Validaciones;
    using System.ComponentModel.DataAnnotations;

    public class UsuarioCreateDTO
    {
        [Required]
        [Cedula(ErrorMessage ="La Cedula ingresada no es valida")]
        public string NumeroDocumentoIdentidad { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electornico es obligatorio")]
        [EmailAddress(ErrorMessage = "EL formato de correo electronico no esvalido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]

        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]) (?=.*[0-9]) (?=.*[!@#$%&^*])", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula, un numero y un caracter especial")]
        public string Clave { get; set; } = string.Empty;
        public int IdRolUsuario { get; set; }
    }

}
