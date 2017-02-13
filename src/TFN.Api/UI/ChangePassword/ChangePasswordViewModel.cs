namespace TFN.Api.UI.ChangePassword
{
    public class ChangePasswordViewModel : ChangePasswordInputModel
    {
        public string ErrorMessage { get; set; }

        public ChangePasswordViewModel(ChangePasswordInputModel other)
        {
            Password = other.Password;
            ConfirmPassword = other.ConfirmPassword;
            ValidationKey = other.ValidationKey;
        }

        public ChangePasswordViewModel()
        {

        }
    }
}