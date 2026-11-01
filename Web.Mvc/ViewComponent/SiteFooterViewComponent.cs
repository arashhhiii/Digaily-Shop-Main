using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.ViewComponent
{
    public class SiteFooterViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("SiteFooterViewComponent");
        }
    }
}
