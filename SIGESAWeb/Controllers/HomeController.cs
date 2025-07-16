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

        // Redirige a Login si el usuario no esta autenticado
        [Authorize(Roles = "Administrador, Usuario, Coordinador")]
        public IActionResult Index()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("Login", "Acceso");
            }

            return View();
        }

        [Authorize(Roles = "Administrador, Usuario")]
        public IActionResult Privacy()
        {
            return View();
        }

        // Pagina de acceso denegado
        [AllowAnonymous]
        public IActionResult Denegado()
        {
            return View(); // Vista personalizada en Views/Home/Denegado.cshtml
        }

        // Cierre de sesión
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
