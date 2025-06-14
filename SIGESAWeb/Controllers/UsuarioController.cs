﻿using Microsoft.AspNetCore.Authorization;
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

        //[HttpGet]
        //public async Task<IActionResult> Lista()
        //{
        //    // List<Usuario> lista = await _repositorio.ObtenerListaAsync();

        //    List<Usuario> lista = await _repositorio.ObtenerListaAsync();

        //    return StatusCode(StatusCodes.Status200OK, new { data = lista });
        //}

        //[HttpPost]
        //public async Task<IActionResult> Guardar([FromBody] Usuario objeto)
        //{
        //    string respuesta = await _repositorio.GuardarAsync(objeto);
        //    return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        //}

        //[HttpPut]
        //public async Task<IActionResult> Editar([FromBody] Usuario objeto)
        //{
        //    //string respuesta = await _repositorio.Editar(objeto);
        //    string respuesta = await _repositorio.EditarAsync(objeto);
        //    return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        //}

        //[HttpDelete]
        //public async Task<ActionResult> Eliminar(int Id)
        //{
        //    int respuesta = await _repositorio.EliminarAsync(Id);
        //    return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        //}
    }
}
