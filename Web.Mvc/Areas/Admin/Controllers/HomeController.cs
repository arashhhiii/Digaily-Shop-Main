using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
