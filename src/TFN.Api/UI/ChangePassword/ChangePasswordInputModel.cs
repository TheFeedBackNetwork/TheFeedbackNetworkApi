using System.ComponentModel.DataAnnotations;
using TFN.Mvc.Attributes;

namespace TFN.Api.UI.ChangePassword
{
    public class ChangePasswordInputModel
    {
        [Required(ErrorMessage = "A new password is required.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Use at least 6 characters.")]
        [PasswordValid(MinLetters = 1, MinDigits = 1, ErrorMessage = "Password must contain at least one number and one letter.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirm password is required. This password MUST match your previously entered password.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Use at least 6 characters.")]
        [Compare("Password", ErrorMessage = "Please enter the same password value as above.")]
        [PasswordValid(MinLetters = 1,MinDigits = 1,ErrorMessage = "Password must contain at least one number and one letter.")]
        public string ConfirmPassword { get; set; }
        public string ChangePasswordKey { get; set; }
    }
}