namespace SigesaWeb.Models.DTOs
{
    public class BotonAccionDTO
    {
        public string Texto { get; set; } = "";
        public string Icono { get; set; } = "fas fa-plus";
        public string Clase { get; set; } = "btn-primary";
        public string TargetModalId { get; set; } = "";
    }

}
