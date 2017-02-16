using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TFN.Mvc.Attributes
{
    public class UsernameValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            const string invalidUrlCharacters = "!&$+,/.;=?@#'<>[]{}()|\\^%\"*";

            var username = value as string;

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