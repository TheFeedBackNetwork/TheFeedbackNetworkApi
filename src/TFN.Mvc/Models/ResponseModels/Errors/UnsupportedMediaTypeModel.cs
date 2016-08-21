namespace TFN.Mvc.Models.ResponseModels.Errors
{
    internal class UnsupportedMediaTypeModel : ErrorModel
    {
        public const string UnsupportedMediaTypeMessage = "Unsupported media type. This API is only capable of consuming JSON. Please provide the content in JSON and specific 'application/json' in the Content-Type header.";

        private UnsupportedMediaTypeModel(string message)
            : base(message)
        {
        }

        internal static UnsupportedMediaTypeModel Create()
        {
            return new UnsupportedMediaTypeModel(UnsupportedMediaTypeMessage);
        }
    }
}