using System.ComponentModel.DataAnnotations;

namespace TFN.Api.UI.ChangePassword
{
    public class ChangePasswordInputModel
    {
        [Required(ErrorMessage = "A new password is required.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Use at least 6 characters including at least one number.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirm password is required. This password MUST match your previously entered password.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Use at least 6 characters including at least one number.")]
        [Compare("Password", ErrorMessage = "Please enter the same password value as above.")]
        public string ConfirmPassword { get; set; }
        public string ValidationKey { get; set; }
    }
}