using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TFN.Mvc.HttpResults;

namespace TFN.Mvc.Filters.ResultFilters
{
    public class ForbiddenResultFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as ForbidResult;

            if (result != null)
            {
                context.Result = new HttpForbiddenResult();
            }
        }
    }
}