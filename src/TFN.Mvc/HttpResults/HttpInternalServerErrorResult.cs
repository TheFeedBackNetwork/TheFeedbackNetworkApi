using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpInternalServerErrorResult : ObjectResult
    {
        public HttpInternalServerErrorResult()
            : base(InternalServerErrorModel.Create())
        {
            StatusCode = 500;
        }
    }
}