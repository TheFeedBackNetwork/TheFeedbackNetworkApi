using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TFN.Api.Models.ModelBinders
{
    public class UsernameQueryModelBinder : IModelBinder
    {
        private const string parameterName = "username";

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!String.IsNullOrWhiteSpace(bindingContext.ModelName) &&
                bindingContext.ModelName.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase) &&
                bindingContext.ModelType == typeof(string) &&
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName) != null)
            {
                var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue as string;

                if (!String.IsNullOrWhiteSpace(val))
                {
                    bindingContext.Result = ModelBindingResult.Success(val);
                    return Task.FromResult(0);
                }
                
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.FromResult(0);
        }
    }
}