using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminDashboardController : Controller
    {
        private readonly IDashboardRespositorio _dashboardRepo;

        public AdminDashboardController(IDashboardRespositorio dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }

        public async Task<IActionResult> Index()
        {
            var modelo = new
            {
                TotalReservas = await _dashboardRepo.ObtenerTotalReservasAsync(),
                EspaciosActivos = await _dashboardRepo.ObtenerEspaciosActivosAsync(),
                ReservasCanceladas = await _dashboardRepo.ObtenerReservasCanceladasAsync(),
                IncidenciasReportadas = await _dashboardRepo.ObtenerIncidenciasReportadasAsync()
            };

            return View(modelo);
        }
    }
}
