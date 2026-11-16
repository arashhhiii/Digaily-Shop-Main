using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Web.Mvc.Controllers
{
    public class HomeController : SideBaseCopntroller
    {
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            if (Request.Query.ContainsKey("accountCenter") && User.Identity?.IsAuthenticated == true)
            {
                ViewData["AccountCenterUser"] = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? User.Identity.Name;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
