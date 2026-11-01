using Application.Class.Service.Interfaces;
using Data.Class.DTOs.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Mvc.Controllers
{
    public class AccountController(IUserService userService) : SideBaseCopntroller
    {
        #region CTOR
        private readonly IUserService _userService = userService;
        private readonly ISmsService _smsService;
        private readonly ICaptchaValidator _CaptchaValidator;
        #endregion
        #region Register
        [HttpGet("register")]
        public async Task<IActionResult> RegisterOrLogin(string Mobile, string? returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Mobile"] = Mobile;
            return View();
        }
        [HttpPost("register"), ValidateAntiForgeryToken] 
        public async Task<IActionResult> RegisterOrLogin(registerUserDto dto)
        {
            await _userService.RegisterOrLoginUser(dto);
            return RedirectToAction("MobileAuthorization", new {retutnUrl = dto.returnUrl, Mobile = dto.PhoneNumber});
        }
        #endregion
        #region MobileAuthorization
        [HttpGet("authorization")]
        public async Task<IActionResult> MobileAuthorization(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("authorization"), ValidateAntiForgeryToken]
        public async Task<IActionResult> MobileAuthorization(MobileActivationDTOs dto)
        {
            #region CaptchValidation
            if (!await _CaptchaValidator.IsCaptchaPassedAsync(dto.Token))
            {
                TempData[ErrorMessage] = "authorization weas not successsed";
                return View(dto);
            }
            #endregion
            if (ModelState.IsValid)
            {
                var res = await _userService.CheckMobileAuthorization(dto);
                if (!res)
                {
                    TempData[ErrorMessage] = "the code is noit true";
                    return View(dto);
                }
                var user = await _userService.GetUSerByMobile(dto.Mobile);
                if (user == null) return NotFound();
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.MobileNumber),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(principle, properties);
                TempData[SuccessMessage] = "Welcome";
                if (string.IsNullOrEmpty(dto.returnUrl) && Url.IsLocalUrl(dto.returnUrl)){
                    return Redirect(dto.returnUrl);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                }
            }
            TempData[ErrorMessage] = "please fix the errors";
            return View(dto);
        }
        #endregion
        #region Resend Verification Code
        [HttpGet("ResendVerificationCode")]
        public async Task<IActionResult> ResendVerificationCode(string MobileNumber)
        {

            
            var res = await _userService.SendActivationSms(MobileNumber);
            if(res) return RedirectToAction("MobileAuthorization");
            TempData[ErrorMessage] = "User Phone Number NOt FDound";
            return RedirectToAction("MobileAuthorization");

        }
        #endregion
        #region Log Out
        [Route("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion


    }
}
