using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    public class BitacoraController : Controller
    {

        private readonly IBitacoraRepositorio _repositorio;

        public BitacoraController(IBitacoraRepositorio repositorio)
        {
            _repositorio = repositorio;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
