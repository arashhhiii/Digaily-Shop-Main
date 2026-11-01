using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Data.Class.DTOs.Account
{
    
    public class registerUserDto : CaptchaDto
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "please fill the {0}")]
        [MaxLength(11, ErrorMessage = "phone number cant be more than {1} number {0}")]
        [MinLength(11, ErrorMessage = "phone number cant be less than {1} number {0}")]
        public string PhoneNumber { get; set; }
        public string? returnUrl
        {
            get;
            set;
        }

    }
   
}
