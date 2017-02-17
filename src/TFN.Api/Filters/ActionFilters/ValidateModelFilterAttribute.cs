using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using TFN.Mvc.HttpResults;

namespace TFN.Api.Filters.ActionFilters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var fields = context
                    .ModelState
                    .Where(x => !String.IsNullOrWhiteSpace(x.Key))
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(x => x.Key, y => y.Value.Errors.Select(e => String.IsNullOrWhiteSpace(e.ErrorMessage) ? "This field is in the incorrect format and could not be parsed." : e.ErrorMessage).Distinct());

                if (fields != null && fields.Any())
                {
                    context.Result = new HttpBadRequestResult(fields);
                }
                else
                {
                    context.Result = new HttpBadRequestResult();
                }
            }

            base.OnActionExecuting(context);
        }
    }
}