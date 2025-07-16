using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class NotificacionController : Controller
    {
        private readonly INotificacionRepositorio _repositorio;

        public NotificacionController(INotificacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            var notificaciones = await _repositorio.ObtenerPorUsuarioAsync(idUsuario);

            return View(notificaciones);
        }

        [HttpPost]
        public async Task<IActionResult> MarcarComoLeida(int id)
        {
            var resultado = await _repositorio.MarcarComoLeidaAsync(id);
            return Json(new { data = resultado });
        }

        [HttpPost]
        public async Task<IActionResult> MarcarTodas()
        {
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var resultado = await _repositorio.MarcarTodasComoLeidasAsync(idUsuario);
            return Json(new { data = resultado });
        }
    }
}
