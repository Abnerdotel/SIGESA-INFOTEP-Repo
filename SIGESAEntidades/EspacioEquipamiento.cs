
namespace SigesaEntidades
{
    public class EspacioEquipamiento
    {
        public int IdEspacioEquipamiento { get; set; }
        public int IdEspacio { get; set; }
        public Espacio Espacio { get; set; } = null!;

        public int IdEquipamiento { get; set; }
        public Equipamiento Equipamiento { get; set; } = null!;

        public string FechaCreacion { get; set; } = null!;
    }
}
