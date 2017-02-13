using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TFN.Api.UI.Base
{
    // ReSharper disable once InconsistentNaming
    public class UIController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if(!context.HttpContext.Request.PathBase.Equals("/identity"))
            {
                context.Result = NotFound();
            }
            
        }
    }
}