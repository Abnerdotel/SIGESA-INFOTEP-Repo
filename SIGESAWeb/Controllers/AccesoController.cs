using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SigesaData.Contrato;
using SigesaEntidades;
using SigesaWeb.Models.DTOs;
using SigesaWeb.Models.DTOS;
using SigesaData.Utilidades;
using System.Security.Claims;

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

        public IActionResult Login()
        {
            var claimUser = HttpContext.User;
            

            if (claimUser.Identity != null && claimUser.Identity.IsAuthenticated)
            {
                string rol = claimUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";
                return rol switch
                {
                    "Administrador" => RedirectToAction("Index", "Home"),
                    "Usuario" => RedirectToAction("Index", "Home"),
                    "Coordinador" => RedirectToAction("Index", "Home"),
                    _ => RedirectToAction("Login")
                };
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMUsuarioLogin modelo)
        {
            
            if (string.IsNullOrWhiteSpace(modelo.DocumentoIdentidad) || string.IsNullOrWhiteSpace(modelo.Clave))
            {
                ViewData["Mensaje"] = "Debe ingresar todos los campos.";
                return View();
            }

            Usuario? usuario = await _usuarioRepositorio.AutenticarAsync(modelo.DocumentoIdentidad, modelo.Clave);

            if (usuario == null)
            {
                ViewData["Mensaje"] = "Credenciales incorrectas.";
                return View();
            }

            // Asegurarse de que los roles estén cargados
            var rolAsignado = usuario.Roles
                .Select(r => r.RolUsuario?.Nombre)
                .FirstOrDefault() ?? "Usuario";

            // Crear Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, rolAsignado)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true
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

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(VMUsuario modelo)
        {
            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden.";
                return View();
            }

            Usuario nuevoUsuario = new()
            {
                NumeroDocumentoIdentidad = modelo.DocumentoIdentidad,
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Correo = modelo.Correo,            
                Clave = modelo.Clave,
                FechaCreacion = DateTime.Now
            };

            int idGenerado = await _usuarioRepositorio.GuardarAsync(nuevoUsuario);

            if (idGenerado > 0)
            {
                Rol nuevoRol = new()
                {
                    IdUsuario = idGenerado,
                    IdRolUsuario = 3, // Usuario normal
                    FechaCreacion = DateTime.Now
                };

                await _rolRepositorio.GuardarAsync(nuevoRol);

                ViewBag.Creado = true;
                ViewBag.Mensaje = "Cuenta creada correctamente.";
                return View();
            }

            ViewBag.Mensaje = "No se pudo registrar el usuario.";
            return View();
        }

        public IActionResult Denegado()
        {
            return View();
        }
    }
}
