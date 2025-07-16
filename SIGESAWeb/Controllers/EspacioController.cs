using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EspacioController : Controller
    {
        private readonly IEspacioRepositorio _repositorio;

        public EspacioController(IEspacioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        // Vista principal
        public IActionResult Index() => View();

        // JSON para DataTables
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _repositorio.ObtenerListaAsync();

            var data = lista.Select(e => new
            {
                idEspacio = e.IdEspacio,
                nombre = e.Nombre,
                capacidad = e.Capacidad,
                observaciones = e.Observaciones,
                tipo = e.Tipo.Nombre,
                idTipoEspacio = e.IdTipoEspacio,
                fechaCreacion = e.FechaCreacion.ToString("dd/MM/yyyy")
            });

            return Json(new { data });
        }

        // Guardar nuevo
        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] Espacio modelo)
        {
            try
            {
                modelo.FechaCreacion = DateTime.Now;
                var id = await _repositorio.GuardarAsync(modelo);
                return Json(new { data = id > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { data = false, error = ex.Message });
            }
        }

        // Editar existente
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Espacio modelo)
        {
            try
            {
                var exito = await _repositorio.EditarAsync(modelo);
                return Json(new { data = exito });
            }
            catch (Exception ex)
            {
                return Json(new { data = false, error = ex.Message });
            }
        }

        // Eliminar
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var exito = await _repositorio.EliminarAsync(id);
                return Json(new { data = exito });
            }
            catch (Exception ex)
            {
                return Json(new { data = false, error = ex.Message });
            }
        }
    }
}
