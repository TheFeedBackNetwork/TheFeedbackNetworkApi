using System;
using System.ComponentModel;
using System.Globalization;
using TFN.Api.Models.QueryModels;

namespace TFN.Api.Models.Converters
{
    public class ExcludeQueryConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                ExcludeQueryModel model;
                if (ExcludeQueryModel.TryParse((string)value, out model))
                {
                    return model;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
