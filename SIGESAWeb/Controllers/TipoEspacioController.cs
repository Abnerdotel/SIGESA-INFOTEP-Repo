using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TipoEspacioController : Controller
    {
        private readonly ITipoEspacioRepositorio _repositorio;

        public TipoEspacioController(ITipoEspacioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _repositorio.ObtenerListaAsync();
            var data = lista.Select(t => new {
                idTipoEspacio = t.IdTipoEspacio,
                nombre = t.Nombre,
                fechaCreacion = t.FechaCreacion.ToString("dd/MM/yyyy")
            });

            return Json(new { data });
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] TipoEspacio modelo)
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

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] TipoEspacio modelo)
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
