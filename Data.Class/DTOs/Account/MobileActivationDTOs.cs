using System.ComponentModel.DataAnnotations;

namespace Data.Class.DTOs.Account
{
    public class MobileActivationDTOs : CaptchaDto
    {
        [Required]
        [RegularExpression(@"^\d$")]
        public string activationCodePart1 { get; set; }
        [Required]
        [RegularExpression(@"^\d$")]
        public string activationCodePart2 { get; set; }
        [Required]
        [RegularExpression(@"^\d$")]
        public string activationCodePart3 { get; set; }
        [Required]
        [RegularExpression(@"^\d$")]
        public string activationCodePart4 { get; set; }
        [Required]
        [RegularExpression(@"^\d$")]
        public string activationCodePart5 { get; set; }
        [Required]
        [RegularExpression(@"^\d$")]
        public string activationCodePart6 { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string? returnUrl { get; set; }
    }
    public enum ActivationResult
    {
        success, 
        Error
    }
}
