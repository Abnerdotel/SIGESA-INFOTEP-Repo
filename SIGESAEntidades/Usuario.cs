using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SigesaEntidades
{

    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
       // [Cedula(ErrorMessage = "La Cedula ingresada no es valida")]

        public string NumeroDocumentoIdentidad { get; set; } = null!;

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Apellido { get; set; } = null!;


        [Required(ErrorMessage = "El correo electornico es obligatorio")]
        [EmailAddress(ErrorMessage = "EL formato de correo electronico no esvalido")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Z]) (?=.*[0-9]) (?=.*[!@#$%&^*])", ErrorMessage = "La contraseña debe tener al menos una letra mayuscula, un numero y un caracter especial")]
        public string Clave { get; set; } = null!;

        [Required]

        public bool EstaActivo { get; set; } = true;

        public DateTime FechaCreacion { get; set; }

        // Relacion muchos a muchos a travas de Rol
        public virtual ICollection<Rol> Roles { get; set; } = new List<Rol>();

        // Otras relaciones del sistema
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
        public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
    }



}
