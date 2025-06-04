

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }       
        public string Modulo { get; set; } = null!;
    
        public string Accion { get; set; } = null!;     
        public string Detalle { get; set; } = null!;     
        public DateTime FechaAccion { get; set; }

        // Relación opcional con Usuario

        public int IdUsuario { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
    }

}
