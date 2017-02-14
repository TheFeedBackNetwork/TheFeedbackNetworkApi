namespace TFN.Api.UI.VerifyAccount
{
    public class VerifyViewModel : VerifyInputModel
    {
        public string ErrorMessage { get; set; }

        public VerifyViewModel(VerifyInputModel other)
        {
            VerifyPassword = other.VerifyPassword;
            EmailVerificationKey = other.EmailVerificationKey;
        }

        public VerifyViewModel()
        {

        }
    }
}