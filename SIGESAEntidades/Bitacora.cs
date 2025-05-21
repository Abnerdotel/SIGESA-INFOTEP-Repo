

namespace SigesaEntidades
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }
        public string Modulo { get; set; } = null!;
        public string Accion { get; set; } = null!;
        public string Detalle { get; set; } = null!;
        public string FechaAccion { get; set; } = null!;
        public string UsuarioAccion { get; set; } = null!;
    }
}
