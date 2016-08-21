using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TFN.Mvc.HttpResults;

namespace TFN.Mvc.Filters.ResultFilters
{
    public class UnAuthorisedResultFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as UnauthorizedResult;

            if (result != null)
            {
                context.Result = new HttpUnauthorisedResult();
            }
        }
    }
}