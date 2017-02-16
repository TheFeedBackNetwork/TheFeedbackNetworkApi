using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TFN.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordValidAttribute : ValidationAttribute
    {
        public int MinLetters { get; set; }
        public int MinDigits { get; set; }

        public PasswordValidAttribute()
        {
            
        }

        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
            {
                return false;
            }

            var enoughDigits = password.Count(char.IsDigit) >= MinDigits;
            var enoughLetters = password.Count(char.IsLetter) >= MinLetters;

            return enoughDigits && enoughLetters;

        }
    }
}