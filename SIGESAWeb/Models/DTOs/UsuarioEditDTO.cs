using SigesaWeb.Models.Validaciones;

using System.ComponentModel.DataAnnotations;


namespace SigesaWeb.Models.DTOs
    {
    public class UsuarioEditDTO
    {
        public int IdUsuario { get; set; }

        [Required]
        [Cedula(ErrorMessage = "La Cedula ingresada no es valida")]
        public string NumeroDocumentoIdentidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;


        [Required(ErrorMessage = "El correo electornico es obligatorio")]
        [RegularExpression(@"^\S*$", ErrorMessage = "El correo no puede tener espacios en blanco")]
        [EmailAddress(ErrorMessage = "EL formato de correo electronico no esvalido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]) (?=.*[0-9]) (?=.*[!@#$%&^*])", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula, un numero y un caracter especial")]
        public string Clave { get; set; } = null!;
        public int IdRolUsuario { get; set; }
    }

}

