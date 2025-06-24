using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;

namespace SigesaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _repositorio;
        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
         
            IEnumerable<Usuario> lista = await _repositorio.ObtenerListaAsync();
         

            var usuarios = await _repositorio.ObtenerListaAsync();

            var resultado = usuarios.Select(u => new
            {
                idUsuario = u.IdUsuario,
                numeroDocumentoIdentidad = u.NumeroDocumentoIdentidad,
                nombre = u.Nombre,
                apellido = u.Apellido,
                correo = u.Correo,
               // clave = "", // Por seguridad, nunca devuelvas la clave real
                fechaCreacion = u.FechaCreacion.ToString("dd/MM/yyyy")
            });

            return Json(new { data = resultado });
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] Usuario objeto)
        {


            int respuesta = await _repositorio.GuardarAsync(objeto);
            return StatusCode(StatusCodes.Status200OK, new { data = respuesta });


        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Usuario objeto)
        {
            bool respuesta = await _repositorio.EditarAsync(objeto);
            return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        }

        [HttpDelete]
        public async Task<ActionResult> Eliminar(int Id)
        {
            bool respuesta = await _repositorio.EliminarAsync(Id);
           return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        }
    }
}
