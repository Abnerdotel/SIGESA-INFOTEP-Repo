

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SigesaEntidades
{
    public class TipoEspacio
    {
        public int IdTipoEspacio { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public virtual ICollection<Espacio> Espacios { get; set; } = new List<Espacio>();
    }

}
