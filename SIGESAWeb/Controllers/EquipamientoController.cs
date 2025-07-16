using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class EquipamientoController : Controller
    {
        private readonly IEquipamientoRepositorio _repositorio;

        public EquipamientoController(IEquipamientoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        // Vista principal
        public IActionResult Index() => View();

        // Obtener lista para DataTable
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _repositorio.ObtenerListaAsync();

            var data = lista.Select(e => new
            {
                idEquipamiento = e.IdEquipamiento,
                nombre = e.Nombre,
                descripcion = e.Descripcion,
                fechaCreacion = e.FechaCreacion.ToString("dd/MM/yyyy")
            });

            return Json(new { data });
        }

        // Guardar nuevo
        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] Equipamiento modelo)
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

        // Editar
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Equipamiento modelo)
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
