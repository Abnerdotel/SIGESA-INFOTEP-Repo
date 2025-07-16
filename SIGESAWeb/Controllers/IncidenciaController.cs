using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Coordinador")]
    public class IncidenciaController : Controller
    {
        private readonly IIncidenciaRepositorio _repositorio;

        public IncidenciaController(IIncidenciaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IActionResult> Index()
        {
            int idUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var incidencias = await _repositorio.ObtenerIncidenciasPorUsuarioAsync(idUsuario);
            return View(incidencias);
        }

        [HttpPost]
        public async Task<IActionResult> Reportar(int idEspacio, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) return BadRequest("Descripción requerida.");

            var bitacora = new Bitacora
            {
                Modulo = "Incidencia",
                Accion = "Reporte",
                Detalle = $"Espacio ID {idEspacio}: {descripcion}",
                IdUsuario = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0"),
                FechaAccion = DateTime.Now
            };

            int id = await _repositorio.ReportarIncidenciaAsync(bitacora);
            return Ok(new { data = id });
        }

        [HttpPost]
        public async Task<IActionResult> Resolver(int id)
        {
            var resultado = await _repositorio.ResolverIncidenciaAsync(id);
            return Ok(new { data = resultado });
        }
    }
}
