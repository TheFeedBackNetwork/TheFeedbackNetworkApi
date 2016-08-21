using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpUnauthorisedResult : ObjectResult
    {
        public HttpUnauthorisedResult()
            : base(UnauthorisedModel.Create())
        {
            StatusCode = 401;
        }
    }
}