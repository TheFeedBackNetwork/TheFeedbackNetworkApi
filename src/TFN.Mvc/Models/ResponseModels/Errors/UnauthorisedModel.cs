namespace TFN.Mvc.Models.ResponseModels.Errors
{
    public class UnauthorisedModel : ErrorModel
    {
        private const string UnauthorisedMessage = "Authentication failed. The request must include a valid and non-expired bearer token in the Authorization header.";

        private UnauthorisedModel(string message)
            : base(message)
        {
        }

        public static UnauthorisedModel Create()
        {
            return new UnauthorisedModel(UnauthorisedMessage);
        }
    }
}