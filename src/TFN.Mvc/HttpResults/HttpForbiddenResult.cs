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

        public HttpForbiddenResult(string message)
            : base(ForbiddenModel.Create(message))
        {
            StatusCode = 403;
        }
    }
}