using Microsoft.AspNetCore.Mvc;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpNotAcceptableResult : ObjectResult
    {
        public HttpNotAcceptableResult()
            : base(NotAcceptableModel.Create())
        {
            StatusCode = 406;
        }
    }
}