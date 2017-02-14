using System;
using System.ComponentModel.DataAnnotations;

namespace TFN.Api.UI.Register
{
    public class RegisterInputModel
    {
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Username has to be between 3 and 16 characters long.")]
        public string RegisterUsername { get; set; }

        [Required(ErrorMessage = "An email address is required.")]
        [EmailAddress(ErrorMessage = "A valid email address is required.")]
        public string RegisterEmail { get; set; }
    }
}