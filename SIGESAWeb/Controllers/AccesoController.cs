﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;
using SigesaWeb.Models.DTOS;
using System.Security.Claims;
using SigesaWeb.Models.DTOs;

namespace SigesaWeb.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioRepositorio _repositorio;
        public AccesoController(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity!.IsAuthenticated)
            {
                string rol = claimuser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault()!;
                if (rol == "Administrador") return RedirectToAction("Index", "Home");
                if (rol == "Usuario") return RedirectToAction("Index", "Cursos");
                if (rol == "Coordinador") return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VMUsuarioLogin modelo)
        {
            if (modelo.DocumentoIdentidad == null || modelo.Clave == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            //Usuario usuario_encontrado = await _repositorio.AutenticarAsync(modelo.DocumentoIdentidad, modelo.Clave);
            Usuario usuario_encontrado = await _repositorio.AutenticarAsync(modelo.DocumentoIdentidad, modelo.Clave);

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            ViewData["Mensaje"] = null;

            //aqui guarderemos la informacion de nuestro usuario
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{usuario_encontrado.Nombre} {usuario_encontrado.Apellido}"),
                new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.IdUsuario.ToString()),
                //new Claim(ClaimTypes.Role,usuario_encontrado.RolUsuario.Nombre)
            };


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            string rol = usuario_encontrado.Nombre;
            if (rol == "Usuario") return RedirectToAction("Index", "Cursos");
            if (rol == "Administrador") return RedirectToAction("CursosAsginados", "Cursos");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registrarse()
        {
            return View();
        }
       // [HttpPost]
        //    public async Task<IActionResult> Registrarse(VMUsuario modelo)
        //    {
        //        if (modelo.Clave != modelo.ConfirmarClave)
        //        {
        //            ViewBag.Mensaje = "Las contraseñas no coinciden";
        //            return View();
        //        }

        //        Usuario objeto = new Usuario()
        //        {
        //            NumeroDocumentoIdentidad = modelo.DocumentoIdentidad,
        //            Nombre = modelo.Nombre,
        //            Apellido = modelo.Apellido,
        //            Correo = modelo.Correo,
        //            Clave = modelo.Clave,
        //            RolUsuario = new RolUsuario()
        //            {
        //                IdRolUsuario = 2
        //            }
        //        };
        //        bool resultado = await _repositorio.EditarAsync(objeto);
        //        ViewBag.Mensaje = resultado;
        //        if (resultado == true)
        //        {
        //            ViewBag.Creado = true;
        //            ViewBag.Mensaje = "Su cuenta ha sido creada.";
        //        }
        //        return View();
        //    }

        //    public IActionResult Denegado()
        //    {
        //        return View();
        //    }
        //}

    } 
}
