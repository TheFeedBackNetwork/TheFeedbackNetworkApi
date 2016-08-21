using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TFN.Mvc.Models.ResponseModels.Errors;

namespace TFN.Mvc.HttpResults
{
    public class HttpBadRequestResult : BadRequestObjectResult
    {
        public HttpBadRequestResult()
           : base(BadRequestModel.Create())
        {
        }

        public HttpBadRequestResult(IDictionary<string, IEnumerable<string>> fields)
            : base(BadRequestModel.Create(fields))
        {
        }
    }
}