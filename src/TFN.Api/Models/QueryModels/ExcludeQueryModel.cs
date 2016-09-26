using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TFN.Api.Models.Converters;

namespace TFN.Api.Models.QueryModels
{
    [TypeConverter(typeof(ExcludeQueryConverter))]
    public class ExcludeQueryModel
    {
        public IReadOnlyCollection<string> Attributes { get; private set; }

        public ExcludeQueryModel(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return;
            }

            Attributes = value.Split(',').Select(x => x.ToLower().Trim(',').Trim()).ToList();

            if (Attributes.Any(x => !x.Any(Char.IsLetterOrDigit)))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Only alphanumeric characters permitted.");
            }

            if (Attributes.Any(String.IsNullOrWhiteSpace))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "None of the resources can be empty or null.");
            }

            Attributes = Attributes.Select(x => x.Trim()).ToList();
        }

        public static bool TryParse(string s, out ExcludeQueryModel result)
        {
            try
            {
                result = new ExcludeQueryModel(s);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
