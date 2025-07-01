using Microsoft.AspNetCore.Mvc;

namespace SigesaWeb.Controllers
{
    public class AulasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
