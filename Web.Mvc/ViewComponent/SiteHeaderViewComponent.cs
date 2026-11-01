using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.ViewComponent
{
    public class SiteHeaderViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Copmponent/SiteHeader/SiieHeader.cshtml");
        }
    }
}
