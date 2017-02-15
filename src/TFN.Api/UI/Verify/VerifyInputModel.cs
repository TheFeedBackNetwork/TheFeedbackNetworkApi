using System.ComponentModel.DataAnnotations;
using TFN.Mvc.Attributes;

namespace TFN.Api.UI.Verify
{
    public class VerifyInputModel
    {
        [Required]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [PasswordValid(MinLetters = 1, MinDigits = 1, ErrorMessage = "Password must contain at least one number and one letter.")]
        public string VerifyPassword { get; set; }
        public string EmailVerificationKey { get; set; }
    }
}