using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpForbiddenResult : ObjectResult
    {
        public HttpForbiddenResult()
            : base(ForbiddenModel.Create())
        {
            StatusCode = 403;
        }
    }
}