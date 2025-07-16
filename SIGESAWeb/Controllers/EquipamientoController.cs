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

        // Vista principal del módulo
        public IActionResult Index() => View();

        // Obtener lista de equipamientos para DataTable
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

        // Guardar nuevo equipamiento
        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] Equipamiento modelo)
        {
            if (modelo == null || string.IsNullOrWhiteSpace(modelo.Nombre))
                return BadRequest(new { error = "El nombre del equipamiento es obligatorio." });

            var nombreNormalizado = modelo.Nombre.Trim().ToLower();

            var lista = await _repositorio.ObtenerListaAsync();
            var existeDuplicado = lista.Any(e =>
                e.Nombre.Trim().ToLower() == nombreNormalizado);

            if (existeDuplicado)
                return BadRequest(new { error = "Ya existe un equipamiento con ese nombre." });

            try
            {
                modelo.Nombre = modelo.Nombre.Trim();
                modelo.FechaCreacion = DateTime.Now;

                var id = await _repositorio.GuardarAsync(modelo);
                return Ok(new { data = id > 0 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al guardar: " + ex.Message });
            }
        }

        // Editar equipamiento existente
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Equipamiento modelo)
        {
            if (modelo == null || string.IsNullOrWhiteSpace(modelo.Nombre))
                return BadRequest(new { error = "El nombre del equipamiento es obligatorio." });

            var nombreNormalizado = modelo.Nombre.Trim().ToLower();

            var lista = await _repositorio.ObtenerListaAsync();
            var existeDuplicado = lista.Any(e =>
                e.Nombre.Trim().ToLower() == nombreNormalizado &&
                e.IdEquipamiento != modelo.IdEquipamiento);

            if (existeDuplicado)
                return BadRequest(new { error = "Ya existe otro equipamiento con ese nombre." });

            try
            {
                modelo.Nombre = modelo.Nombre.Trim();
                var exito = await _repositorio.EditarAsync(modelo);

                if (!exito)
                    return NotFound(new { error = "No se encontró el equipamiento para editar." });

                return Ok(new { data = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al editar: " + ex.Message });
            }
        }

        // Eliminar equipamiento por ID
        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var exito = await _repositorio.EliminarAsync(id);

                if (!exito)
                    return NotFound(new { error = "No se encontró el equipamiento a eliminar." });

                return Ok(new { data = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al eliminar: " + ex.Message });
            }
        }
    }
}
