using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Class.DTOs.Account
{
    public class MobileActivationDTOs : CaptchaDto
    {
        
        public string activationCodePart1 { get; set; }
        public string activationCodePart2 { get; set; }
        public string activationCodePart3 { get; set; }
        public string activationCodePart4 { get; set; }
        public string activationCodePart5 { get; set; }
        public string activationCodePart6 { get; set; }
        public string Mobile { get; set; }
        public string? returnUrl { get; set; }
    }
    public enum ActivationResult
    {
        success, 
        Error
    }
}
