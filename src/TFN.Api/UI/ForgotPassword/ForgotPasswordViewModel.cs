namespace TFN.Api.UI.ForgotPassword
{
    public class ForgotPasswordViewModel : ForgotPasswordInputModel
    {
        public string ErrorMessage { get; set; }

        public ForgotPasswordViewModel(ForgotPasswordInputModel other)
        {
            ForgotPasswordEmail = other.ForgotPasswordEmail;
        }

        public ForgotPasswordViewModel()
        {

        }
    }
}