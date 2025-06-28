using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;
using SigesaWeb.Models.DTOS;
using SigesaWeb.Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SigesaWeb.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;

        public AccesoController(IUsuarioRepositorio usuarioRepositorio, IRolRepositorio rolRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _rolRepositorio = rolRepositorio;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Evitar mostrar el login si ya hay sesión activa
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var rol = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";

                // Evita bucle si ya está en Login
                if (ControllerContext.RouteData.Values["action"]?.ToString()?.ToLower() != "login")
                {
                    return rol switch
                    {
                        "Administrador" => RedirectToAction("Index", "Home"),
                        "Usuario" => RedirectToAction("Index", "Home"),
                        "Coordinador" => RedirectToAction("Index", "Home"),
                        _ => RedirectToAction("Login")
                    };
                }
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(VMUsuarioLogin modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.DocumentoIdentidad) || string.IsNullOrWhiteSpace(modelo.Clave))
            {
                ViewData["Mensaje"] = "Debe ingresar todos los campos.";
                return View();
            }

            var usuario = await _usuarioRepositorio.AutenticarAsync(modelo.DocumentoIdentidad, modelo.Clave);

            if (usuario == null)
            {
                ViewData["Mensaje"] = "Credenciales incorrectas.";
                return View();
            }

            var rolAsignado = usuario.Roles
                .Select(r => r.RolUsuario?.Nombre)
                .FirstOrDefault() ?? "Usuario";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, rolAsignado)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true // Mantener la sesión activa
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                authProperties
            );

            return rolAsignado switch
            {
                "Administrador" => RedirectToAction("Index", "Home"),
                "Usuario" => RedirectToAction("Index", "Home"),
                "Coordinador" => RedirectToAction("Index", "Home"),
                _ => RedirectToAction("Login")
            };
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registrarse(VMUsuario modelo)
        {
            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden.";
                return View();
            }

            try
            {
                var nuevoUsuario = new Usuario
                {
                    NumeroDocumentoIdentidad = modelo.DocumentoIdentidad,
                    Nombre = modelo.Nombre,
                    Apellido = modelo.Apellido,
                    Correo = modelo.Correo,
                    Clave = modelo.Clave,
                    EstaActivo = true,
                    FechaCreacion = DateTime.Now,
                    Roles = new List<Rol>
                    {
                        new Rol
                        {
                            IdRolUsuario = 3, // Usuario común por defecto
                            FechaCreacion = DateTime.Now
                        }
                    }
                };

                int idGenerado = await _usuarioRepositorio.GuardarAsync(nuevoUsuario);

                if (idGenerado > 0)
                {
                    ViewBag.Creado = true;
                    ViewBag.Mensaje = "Cuenta creada correctamente.";
                    return View();
                }

                ViewBag.Mensaje = "No se pudo registrar el usuario.";
                return View();
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            catch
            {
                ViewBag.Mensaje = "Error inesperado al registrar el usuario.";
                return View();
            }
        }

        [Authorize]
        public IActionResult Denegado()
        {
            return View();
        }
    }
}
