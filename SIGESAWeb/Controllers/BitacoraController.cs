using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;

namespace SigesaWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class BitacoraController : Controller
    {
        private readonly IBitacoraRepositorio _repositorio;

        public BitacoraController(IBitacoraRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        


        public async Task<IActionResult> Index(string? modulo = null, string? usuario = null, DateTime? desde = null, DateTime? hasta = null)
        {
            var lista = await _repositorio.ObtenerListaAsync(modulo, usuario, desde, hasta);
            return View(lista);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            var bitacora = await _repositorio.ObtenerPorIdAsync(id);
            if (bitacora == null)
                return NotFound();

            return View(bitacora);
        }


        
    }
}
