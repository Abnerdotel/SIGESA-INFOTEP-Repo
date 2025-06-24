using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    public class CursosController : Controller
    {
        private readonly IUsuarioRepositorio _repositorio;


        public CursosController(IUsuarioRepositorio repositorio) {

            _repositorio = repositorio;
        }

        [Authorize(Roles = "Usuario")]
        public IActionResult Index()
        {
            return View();
        }


    }
}
