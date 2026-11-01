using Microsoft.AspNetCore.Mvc;
namespace Web.Mvc.ViewComponent
{
    public class HomeCartViewer : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke() => View("~/Views/Shared/Copmponent/HomeCartViewer/HomeCartViewer.cshtml");
    }
}
