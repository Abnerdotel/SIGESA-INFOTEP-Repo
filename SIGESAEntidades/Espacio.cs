﻿


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SigesaEntidades
{
    public class Espacio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEspacio { get; set; }
        public string Nombre { get; set; } = null!;
        public int Capacidad { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdTipoEspacio { get; set; }
        [ForeignKey("IdTipoEspacio")]
        public virtual TipoEspacio Tipo { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<EspacioEquipamiento> EspacioEquipamientos { get; set; } = new List<EspacioEquipamiento>();
    }

}
