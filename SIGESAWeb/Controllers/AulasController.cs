using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Coordinador")]
    public class AulasController : Controller
    {
        private readonly IAulaRepositorio _repositorio;

        public AulasController(IAulaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            // Suponiendo que puedes obtener el ID del coordinador desde Claims
            int idCoordinador = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var aulas = await _repositorio.ObtenerAulasAsignadasAsync(idCoordinador);
            return View(aulas);
        }
    }
}
