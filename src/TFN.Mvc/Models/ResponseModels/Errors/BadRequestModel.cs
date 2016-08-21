using System.Collections.Generic;

namespace TFN.Mvc.Models.ResponseModels.Errors
{
    internal class BadRequestModel : ErrorModel
    {
        public const string BadRequestMessage = "The request has invalid or missing fields.";

        private BadRequestModel(string message)
            : base(message)
        {
        }

        private BadRequestModel(string message, IDictionary<string, IEnumerable<string>> fields)
            : base(message, fields)
        {
        }

        internal static BadRequestModel Create()
        {
            return new BadRequestModel(BadRequestMessage);
        }

        internal static BadRequestModel Create(IDictionary<string, IEnumerable<string>> fields)
        {
            return new BadRequestModel(BadRequestMessage, fields);
        }
    }
}