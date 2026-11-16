using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Areas.Admin.ViewComponent
{
    public class AdminHeaderViewComponent : global::Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}
