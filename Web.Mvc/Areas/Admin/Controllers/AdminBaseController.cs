using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Mvc.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminEmail")]
    [Area("Admin")]
    public class AdminBaseController : Controller
    {
        protected string ErrorMessage = "Error Message";
        protected string SuccessMessage = "Success Message";
        protected string infoMessage = "Info Message";
        protected string WarningMessage = "Warning Message";
        protected string successtext = "opration was successfull";
        protected string errrotext = "opration has been failed";
        protected string  deletetext= "the opration has bneen deleted";
        protected string imagenotsavedtext = "the image foramt is not corrrect";

    }
}
