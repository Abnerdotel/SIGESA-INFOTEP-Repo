using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    public class NotificacionController : Controller
    {
        private readonly INotificacionRepositorio _repositorio;

        public NotificacionController(INotificacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
