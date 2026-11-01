using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Data.Class.DTOs.Account
{
    

    public class EditUserInfoDTOs :CaptchaDto
    {
        public long userId { get; set; }
        [Display(Name = "FullName")]
        [Required(ErrorMessage = "")]
        [MaxLength(20, ErrorMessage ="the name couldnt be more than a {1} Charecter")]
        public string FullName
        {
            get;
            set;
        }
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "the Email couldnt be more than a {1} Charecter")]
        public string? Email
        {
            get;
            set;
        }
        [Display(Name = "ADDRESS")]
        [Required(ErrorMessage = "")]
        [MaxLength(250, ErrorMessage = "the Address couldnt be more than a {1} Charecter")]
        public string Adress
        {
            get;
            set;
        }
        [Display(Name = "Posatl Code")]
        [Required(ErrorMessage = "")]
        [MaxLength(10, ErrorMessage = "the Post Code couldnt be more than a {1} Charecter")]
        [MinLength(10, ErrorMessage = "the Post Code couldnt be more than a {1} Charecter")]
        public string postCode
        {
            get;
            set;
        }
    }
}
