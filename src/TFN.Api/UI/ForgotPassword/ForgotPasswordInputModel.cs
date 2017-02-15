using System.ComponentModel.DataAnnotations;

namespace TFN.Api.UI.ForgotPassword
{
    public class ForgotPasswordInputModel
    {
        [EmailAddress(ErrorMessage = "A valid email address is required.")]
        [Required(ErrorMessage = "A valid email address is required.")]
        public string ForgotPasswordEmail { get; set; }
    }
}