using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class ReservaController : Controller
    {
        private readonly IReservaRepositorio _repositorio;

        public ReservaController(IReservaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Historial()
        {
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var historial = await _repositorio.ObtenerHistorialUsuarioAsync(idUsuario);
            return View(historial);
        }

        public async Task<IActionResult> Estado()
        {
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var pendientes = await _repositorio.ObtenerPendientesPorUsuarioAsync(idUsuario);
            return View(pendientes);
        }

        public IActionResult Nueva()
        {
            return View(); // Muestra formulario con calendario
        }

        [HttpPost]
        public async Task<IActionResult> Nueva(Reserva reserva)
        {
            if (!ModelState.IsValid)
                return View(reserva);

            bool disponible = await _repositorio.VerificarDisponibilidadAsync(reserva.IdEspacio, reserva.FechaInicio, reserva.FechaFin);
            if (!disponible)
            {
                ViewBag.Mensaje = "El espacio no está disponible en ese rango de fechas.";
                return View(reserva);
            }

            reserva.IdUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            reserva.IdEstado = 1; // Estado pendiente
            reserva.FechaCreacion = DateTime.Now;

            await _repositorio.GuardarAsync(reserva);
            return RedirectToAction("Historial");
        }
    }
}
