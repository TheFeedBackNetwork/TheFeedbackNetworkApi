namespace TFN.Mvc.Models.ResponseModels.Errors
{
    internal class InternalServerErrorModel : ErrorModel
    {
        public const string NotFoundMessage = "The server has encountered an unexpected internal error.";

        private InternalServerErrorModel(string message)
            : base(message)
        {
        }

        internal static InternalServerErrorModel Create()
        {
            return new InternalServerErrorModel(NotFoundMessage);
        }
    }
}