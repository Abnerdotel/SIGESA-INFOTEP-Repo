
namespace SigesaEntidades
{
    public class EspacioEquipamiento
    {
        int IdEspacioEquipamiento {  get; set; }
        public Espacio Espacio { get; set; } = null!;
        public Equipamiento Equipamiento { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
    }
}
