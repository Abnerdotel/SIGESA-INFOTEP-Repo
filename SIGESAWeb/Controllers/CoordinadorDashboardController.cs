using Microsoft.AspNetCore.Mvc;

namespace SigesaWeb.Controllers
{
    public class CoordinadorDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
