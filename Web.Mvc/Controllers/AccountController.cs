using Application.Class.Service.Interfaces;
using Data.Class.DTOs.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Mvc.Controllers
{
    public class AccountController(IUserService userService, ICaptchaValidator captchaValidator, IWebHostEnvironment environment) : SideBaseCopntroller
    {
        #region CTOR
        private readonly IUserService _userService = userService;
        private readonly ICaptchaValidator _CaptchaValidator = captchaValidator;
        private readonly IWebHostEnvironment _environment = environment;
        #endregion
        #region Register
        [HttpGet("register")]
        public IActionResult RegisterOrLogin(string? Mobile = null, string? returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["Mobile"] = Mobile;
            return View(new registerUserDto { PhoneNumber = Mobile ?? string.Empty, returnUrl = returnUrl });
        }
        [HttpPost("register"), ValidateAntiForgeryToken] 
        public async Task<IActionResult> RegisterOrLogin(registerUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Mobile"] = dto.PhoneNumber;
                ViewData["AuthMethod"] = Request.Form.ContainsKey(nameof(dto.PhoneNumber)) ? "phone" : "email";
                return View(dto);
            }
            if (!await _CaptchaValidator.IsCaptchaPassedAsync(dto.Token))
            {
                TempData[ErrorMessage] = "We could not verify the CAPTCHA. Please try again.";
                ViewData["Mobile"] = dto.PhoneNumber;
                ViewData["AuthMethod"] = Request.Form.ContainsKey(nameof(dto.PhoneNumber)) ? "phone" : "email";
                return View(dto);
            }

            var delivery = await _userService.RegisterOrLoginUserWithCode(dto);
            if (!delivery.SmsSent)
            {
                if (_environment.IsDevelopment() && delivery.DevelopmentCode != null)
                    TempData["DevelopmentActivationCode"] = delivery.DevelopmentCode;
                else
                    TempData[ErrorMessage] = "The activation SMS could not be sent. Please try again later.";
            }
            return RedirectToAction(nameof(MobileAuthorization), new { returnUrl = dto.returnUrl, Mobile = dto.PhoneNumber });
        }
        #endregion
        #region MobileAuthorization
        [HttpGet("authorization")]
        public IActionResult MobileAuthorization(string? Mobile = null, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new MobileActivationDTOs { Mobile = Mobile ?? string.Empty, returnUrl = returnUrl });
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
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim("RoleId", user.RoleId.ToString())
                };
                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(principle, properties);
                TempData[SuccessMessage] = "Welcome";
                if (!string.IsNullOrEmpty(dto.returnUrl) && Url.IsLocalUrl(dto.returnUrl)){
                    return Redirect(dto.returnUrl);
                }
                TempData[SuccessMessage] = "You have entred successfully";
                return RedirectToAction("Index", "Home", new { accountCenter = true });
                
            }
            TempData[ErrorMessage] = "please fix the errors";
            return View(dto);
        }
        #endregion
        #region Resend Verification Code
        [HttpGet("ResendVerificationCode")]
        public async Task<IActionResult> ResendVerificationCode(string MobileNumber, string? returnUrl = null)
        {
            var delivery = await _userService.SendActivationSmsWithCode(MobileNumber);
            if (delivery.SmsSent)
            {
                return RedirectToAction(nameof(MobileAuthorization), new { Mobile = MobileNumber, returnUrl });
            }

            if (_environment.IsDevelopment() && delivery.DevelopmentCode != null)
                TempData["DevelopmentActivationCode"] = delivery.DevelopmentCode;
            else
                TempData[ErrorMessage] = delivery.DevelopmentCode == null
                    ? "The phone number was not found."
                    : "The activation SMS could not be sent. Please try again later.";
            return RedirectToAction(nameof(MobileAuthorization), new { Mobile = MobileNumber, returnUrl });

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
