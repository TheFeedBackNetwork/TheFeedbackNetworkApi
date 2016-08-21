namespace TFN.Mvc.Models.ResponseModels.Errors
{
    internal class NotAcceptableModel : ErrorModel
    {
        public const string NotAcceptableMessage = "Not acceptable. The API is only capable of generating content in JSON. Please specific 'application/json' or '*/*' in the Accept header.";

        private NotAcceptableModel(string message)
            : base(message)
        {
        }

        internal static NotAcceptableModel Create()
        {
            return new NotAcceptableModel(NotAcceptableMessage);
        }
    }
}