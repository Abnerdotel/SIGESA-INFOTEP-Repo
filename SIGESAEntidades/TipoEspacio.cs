

using System.Globalization;

namespace SigesaEntidades
{
    public class TipoEspacio
    {
        public int IdTipoEspacio {  get; set; }
        public string Nombre { get; set; } = null!;

        public string FechaCreacion { get; set; } = null!;
    }
}
