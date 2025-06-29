using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;
using SigesaWeb.Models.DTOs;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var usuarios = await _repositorio.ObtenerListaAsync();

            var resultado = usuarios.Select(u => new
            {
                idUsuario = u.IdUsuario,
                numeroDocumentoIdentidad = u.NumeroDocumentoIdentidad,
                nombre = u.Nombre,
                apellido = u.Apellido,
                correo = u.Correo,
                fechaCreacion = u.FechaCreacion.ToString("dd/MM/yyyy"),
                estaActivo = u.EstaActivo,
                rol = u.Roles.FirstOrDefault()?.RolUsuario?.Nombre ?? "Sin Rol",
                idRolUsuario = u.Roles.FirstOrDefault()?.IdRolUsuario ?? 0
            });

            return Json(new { data = resultado });
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] UsuarioCreateDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    NumeroDocumentoIdentidad = dto.NumeroDocumentoIdentidad,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Correo = dto.Correo,
                    Clave = dto.Clave,
                    EstaActivo = true,
                    FechaCreacion = DateTime.Now,
                    Roles = new List<Rol>
            {
                new Rol { IdRolUsuario = dto.IdRolUsuario }
            }
                };

                int id = await _repositorio.GuardarAsync(usuario);
                return Ok(new { data = id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { data = false, error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { data = false, error = "Error inesperado al guardar el usuario." });
            }
        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] UsuarioEditDTO dto)
        {
            var usuario = new Usuario
            {
                IdUsuario = dto.IdUsuario,
                NumeroDocumentoIdentidad = dto.NumeroDocumentoIdentidad,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Clave = dto.Clave ?? "",
                Roles = new List<Rol>
                {
                    new Rol { IdRolUsuario = dto.IdRolUsuario }
                }
            };

            bool actualizado = await _repositorio.EditarAsync(usuario);
            return Ok(new { data = actualizado });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool eliminado = await _repositorio.EliminarAsync(id);
            return Ok(new { data = eliminado });
        }

        [HttpPut]
        public async Task<IActionResult> CambiarEstado(int id, bool activar)
        {
            bool resultado = await _repositorio.CambiarEstadoAsync(id, activar);
            return Ok(new { data = resultado });
        }
    }
}
