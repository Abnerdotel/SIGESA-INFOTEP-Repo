namespace SigesaWeb.Models.DTOs
{
    public class MetricCardDTO
    {
        public string Titulo { get; set; } = "";
        public string Valor { get; set; } = "";
        public string Icono { get; set; } = "fas fa-chart-bar";
        public string Color { get; set; } = "primary"; // success, danger, info, warning
    }

}
