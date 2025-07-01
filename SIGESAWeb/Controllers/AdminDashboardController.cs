using Microsoft.AspNetCore.Mvc;

namespace SigesaWeb.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
