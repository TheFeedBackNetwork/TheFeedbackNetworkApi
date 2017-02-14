using System.ComponentModel.DataAnnotations;
using TFN.Mvc.Attributes;

namespace TFN.Api.UI.SignIn
{
    public class SignInInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [PasswordValid(MinLetters = 1, MinDigits = 1, ErrorMessage = "Password must contain at least one number and one letter.")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}