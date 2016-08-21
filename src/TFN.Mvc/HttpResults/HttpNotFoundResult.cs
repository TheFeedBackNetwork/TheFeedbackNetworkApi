using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpNotFoundResult : NotFoundObjectResult
    {
        public HttpNotFoundResult()
            : base(NotFoundModel.Create())
        {
        }
    }
}