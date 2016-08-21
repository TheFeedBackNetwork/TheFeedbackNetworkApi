using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpUnsupportedMediaTypeResult : ObjectResult
    {
        public HttpUnsupportedMediaTypeResult()
            : base(UnsupportedMediaTypeModel.Create())
        {
            StatusCode = 415;
        }
    }
}