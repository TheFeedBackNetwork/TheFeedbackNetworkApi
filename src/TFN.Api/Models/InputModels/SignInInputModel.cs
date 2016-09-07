using System.ComponentModel.DataAnnotations;

namespace TFN.Api.Models.InputModels
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