using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TFN.Api.Models.ModelBinders
{
    public class LimitQueryModelBinder : IModelBinder
    {
        private const string parameterName = "limit";

        private const int maximumLimit = 25;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!String.IsNullOrWhiteSpace(bindingContext.ModelName) &&
                bindingContext.ModelName.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase) &&
                bindingContext.ModelType == typeof(short) &&
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName) != null)
            {
                short value;
                var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue as string;

                if (String.IsNullOrWhiteSpace(val))
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.FromResult(0);
                }
                else if (Int16.TryParse(val, out value))
                {
                    if (value > 0 && value <= maximumLimit)
                    {
                        bindingContext.Result = ModelBindingResult.Success(value);
                        return Task.FromResult(0);
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Value '{value}' is invalid. Maximum allowed limit is {maximumLimit}.");
                    }
                }
                else
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Value is invalid. Limit must be a valid integer.");
                }
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.FromResult(0);
        }
    }
}
