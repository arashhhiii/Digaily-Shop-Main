using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Class.DTOs.Account
{
    public class LoginUserDTOs
    {

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "")]
        [MaxLength(12, ErrorMessage = "the name couldnt be more than a {1} Charecter")]
        public string mobileNumber { get; set; }
        public string? returnUrl { get; set; }
    }
    public enum Loginresult
    {
        success,
        UserNotFound
    }
}
