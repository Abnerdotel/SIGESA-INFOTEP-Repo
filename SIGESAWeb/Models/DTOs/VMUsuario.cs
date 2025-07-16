namespace SigesaWeb.Models.DTOS
{
    using SigesaWeb.Models.Validaciones;
    using System.ComponentModel.DataAnnotations;

    public class VMUsuario
    {
        [Required(ErrorMessage = "El documento de identidad es obligatorio")]
        [Cedula(ErrorMessage = "La Cedula ingresada no es valida")]
        public string DocumentoIdentidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatoria")]
        public string Nombre { get; set; } = string.Empty;

        [Required (ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "EL formato de correo electronico no esvalido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]) (?=.*[0-9]) (?=.*[!@#$%&^*])", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula, un numero y un caracter especial")]
        public string Clave { get; set; } = string.Empty;


        [DataType(DataType.Password)]
        [Required(ErrorMessage ="La confirmación es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]) (?=.*[0-9]) (?=.*[!@#$%&^*])", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula, un numero y un caracter especial")]
        [Compare(nameof(Clave), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarClave { get; set; } = string.Empty;
    }

}
