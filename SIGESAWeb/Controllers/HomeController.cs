using SigesaWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIGESA.Models;
using System.Diagnostics;

namespace SigesaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Solo accesible por usuarios con rol Administrador
        [Authorize(Roles = "Administrador, Usuario")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Usuario")]
        public IActionResult Privacy()
        {
            return View();
        }

        // Acceso denegado personalizado
        [AllowAnonymous]
        public IActionResult Denegado()
        {
            return View(); // Vista personalizada: Views/Home/Denegado.cshtml
        }

        // Cierre de sesión (logout)
        [Authorize]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
