namespace SigesaWeb.Models.DTOs
{
    public class TablaGenericaDTO
    {
        public string IdTabla { get; set; } = "tablaGenerica";
        public List<string> Columnas { get; set; } = new();
        public List<List<string>> Filas { get; set; } = new();
    }

}
