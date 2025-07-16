using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Coordinador")]
    public class EquiposController : Controller
    {
        private readonly IEquipoRepositorio _repositorio;

        public EquiposController(IEquipoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            int idCoordinador = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var equipos = await _repositorio.ObtenerEquiposAsignadosAsync(idCoordinador);
            return View(equipos);
        }

        [HttpPost]
        public async Task<IActionResult> ReportarFalla(int idEquipo, string descripcion)
        {
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (string.IsNullOrWhiteSpace(descripcion))
                return BadRequest(new { error = "La descripción de la falla es requerida." });

            var exito = await _repositorio.ReportarFallaAsync(idEquipo, descripcion, idUsuario);
            return Ok(new { data = exito });
        }
    }
}
