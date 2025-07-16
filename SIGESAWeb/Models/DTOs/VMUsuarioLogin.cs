using System.ComponentModel.DataAnnotations;

namespace SigesaWeb.Models.DTOs
{
    public class VMUsuarioLogin
    {

        [Required(ErrorMessage = "El correo electornico es obligatorio")]
        [EmailAddress(ErrorMessage = "EL formato de correo electronico no esvalido")]
        public string Correo { get; set; } = null!;


        [Required(ErrorMessage ="La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage ="La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]) (?=.*[0-9]) (?=.*[!@#$%&^*])", ErrorMessage ="La contraseña debe tener al menos una letra mayuscula, un numero y un caracter especial")]
        public string Clave { get; set; } = null!;


    }
}
