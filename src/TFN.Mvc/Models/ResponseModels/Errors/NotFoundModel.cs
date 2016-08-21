namespace TFN.Mvc.Models.ResponseModels.Errors
{
    public class NotFoundModel : ErrorModel
    {
        private const string NotFoundMessage = "The requested resource does not exist.";

        private NotFoundModel(string message)
            : base(message)
        {
        }

        public static NotFoundModel Create()
        {
            return new NotFoundModel(NotFoundMessage);
        }
    }
}