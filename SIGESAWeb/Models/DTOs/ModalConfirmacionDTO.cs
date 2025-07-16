namespace SigesaWeb.Models.DTOs
{
    public class ModalConfirmacionDTO
    {
        public string IdModal { get; set; } = "modalConfirmacion";
        public string Titulo { get; set; } = "Confirmar acción";
        public string Mensaje { get; set; } = "¿Estás seguro de que deseas continuar?";
        public string IdBotonConfirmar { get; set; } = "btnConfirmar";
    }

}
