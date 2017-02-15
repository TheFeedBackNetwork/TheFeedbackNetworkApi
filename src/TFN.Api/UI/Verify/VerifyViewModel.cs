namespace TFN.Api.UI.Verify
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