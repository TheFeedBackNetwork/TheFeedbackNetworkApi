using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TFN.Mvc.Attributes
{
    public class TagsValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var tags = value as IReadOnlyList<string>;

            if (tags == null)
            {
                return false;
            }

            if (tags.Any(x => !IsUrlSafe(x)))
            {
                return false;
            }

            return true;
        }

        private bool IsUrlSafe(string value)
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