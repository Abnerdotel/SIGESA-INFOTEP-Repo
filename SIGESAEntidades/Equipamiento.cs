
namespace SigesaEntidades
{
    public class Equipamiento
    {
        public int IdEquipamiento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<EspacioEquipamiento> Espacios { get; set; } = new List<EspacioEquipamiento>();
    }

}
