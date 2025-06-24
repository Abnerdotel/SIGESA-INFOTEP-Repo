using Microsoft.AspNetCore.Mvc;
using SigesaEntidades;
using System.Security.Claims;

namespace SigesaWeb.ViewComponents
{
    public class MenuUsuarioViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {

            ClaimsPrincipal claimuser = HttpContext.User;

            string rolUsuario = "";
           // string nombreUsuario = "";
            if (claimuser.Identity!.IsAuthenticated)
            {
                //nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)!.Select(c => c.Value)!.SingleOrDefault()!;
                rolUsuario = claimuser.Claims.Where(c => c.Type== ClaimTypes.Role)!.Select(c => c.Value)!.SingleOrDefault()!;
            }

            ViewData["rolUsuario"] = rolUsuario;
           // ViewData["nombreUsuario"] = nombreUsuario;

            return View();
        }


       


    }
}
