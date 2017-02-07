using System.ComponentModel.DataAnnotations;

namespace TFN.Api.UI.SignIn
{
    public class SignInInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}