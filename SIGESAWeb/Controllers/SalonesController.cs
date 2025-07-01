using Microsoft.AspNetCore.Mvc;

namespace SigesaWeb.Controllers
{
    public class SalonesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
