using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Coordinador")]
    public class CoordinadorDashboardController : Controller
    {
        private readonly IAulaRepositorio _aulaRepo;
        private readonly IReservaRepositorio _reservaRepo;
        private readonly IIncidenciaRepositorio _incidenciaRepo;

        public CoordinadorDashboardController(
            IAulaRepositorio aulaRepo,
            IReservaRepositorio reservaRepo,
            IIncidenciaRepositorio incidenciaRepo)
        {
            _aulaRepo = aulaRepo;
            _reservaRepo = reservaRepo;
            _incidenciaRepo = incidenciaRepo;
        }

        public async Task<IActionResult> Index()
        {
            int idCoordinador = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            var aulasAsignadas = await _aulaRepo.ObtenerAulasAsignadasAsync(idCoordinador);
            var reservas = await _reservaRepo.ObtenerListaAsync();
            var incidencias = await _incidenciaRepo.ObtenerIncidenciasPorUsuarioAsync(idCoordinador);

            var modelo = new
            {
                CantidadAulas = aulasAsignadas.Count(),
                ReservasActivas = reservas.Count(r => r.Estado.Nombre == "Confirmada"),
                Incidencias = incidencias.Count()
            };

            return View(modelo);
        }
    }
}
