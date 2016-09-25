using System;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services.Utilities;

namespace TFN.Domain.Services.Validators
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public IUserService UserService { get; private set; }
        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            UserService = userService;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await UserService.GetAsync(context.UserName, context.Password);

            if (user != null)
            {
                var claims = ClaimsUtility.GetClaims(user);
                context.Result = new GrantValidationResult(user.Id.ToString(), "password",claims);
            }
        }
    }
}
