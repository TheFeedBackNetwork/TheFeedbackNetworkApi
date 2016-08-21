namespace TFN.Mvc.Models.ResponseModels.Errors
{
    public class ForbiddenModel : ErrorModel
    {
        private const string ForbiddenMessage = "Access forbidden. For security reasons, the server refused to fulfill your request.";

        private ForbiddenModel(string message)
            : base(message)
        {
        }

        public static ForbiddenModel Create()
        {
            return new ForbiddenModel(ForbiddenMessage);
        }
    }
}