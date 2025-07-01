using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    public class EquipamientoController : Controller
    {


        private readonly IEquipoRepositorio _repositorio;

        public EquipamientoController(IEquipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
