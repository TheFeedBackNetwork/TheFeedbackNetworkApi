using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TFN.Api.UI.SignIn
{
    public class SignInViewModel : SignInInputModel
    {
        public SignInViewModel(HttpContext httpContext)
        {
            ExternalProviders = httpContext.Authentication.GetAuthenticationSchemes()
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.AuthenticationScheme
                });
        }

        public SignInViewModel(HttpContext httpContext, SignInInputModel other)
            : this(httpContext)
        {
            Username = other.Username;
            Password = other.Password;
            ReturnUrl = other.ReturnUrl;
        }

        public string ErrorMessage { get; set; }
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
    }

    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}