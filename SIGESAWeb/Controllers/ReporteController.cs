using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReporteController : Controller
    {
        private readonly IReporteRepositorio _repositorio;

        public ReporteController(IReporteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExportarReservas(DateTime inicio, DateTime fin)
        {
            var archivo = await _repositorio.GenerarReporteReservasAsync(inicio, fin);

            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Reservas_{inicio:yyyyMMdd}_{fin:yyyyMMdd}.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportarUsuarios()
        {
            var archivo = await _repositorio.GenerarReporteUsuariosAsync();
            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Usuarios.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> ExportarEspacios()
        {
            var archivo = await _repositorio.GenerarReporteEspaciosAsync();
            return File(archivo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte_Espacios.xlsx");
        }
    }
}
