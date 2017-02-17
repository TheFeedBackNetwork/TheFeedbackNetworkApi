using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;

namespace TFN.Mvc.Attributes
{
    public class UsernameOrEmailValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var usernameOrEmail = value as string;

            if (usernameOrEmail == null)
            {
                return false;
            }

            return IsValidUsername(usernameOrEmail) || IsEmail(usernameOrEmail);
        }

        private static bool IsEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidUsername(string value)
        {
            const string invalidUrlCharacters = "!&$+,/.;=?@#'<>[]{}()|\\^%\"*";

            var username = value;

            if (username == null)
            {
                return false;
            }

            if (invalidUrlCharacters.Any(c => username.Contains(c.ToString())))
            {
                return false;
            }

            return true;
        }

    }
}