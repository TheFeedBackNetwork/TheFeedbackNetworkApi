using System.Collections.Generic;

namespace TFN.Mvc.Models.ResponseModels.Errors
{
    /// <summary>
    /// The error response model.
    /// </summary>
    public abstract class ErrorModel
    {
        public string Message { get; private set; }
        public IDictionary<string, IEnumerable<string>> Fields { get; set; }

        public ErrorModel(string message)
        {
            Message = message;
        }

        public ErrorModel(string message, IDictionary<string, IEnumerable<string>> fields)
        {
            Message = message;
            Fields = fields;
        }
    }
}