using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TFN.Api.Authorization.Models.Resource;

namespace TFN.Api.Authorization.Handlers
{
    public class CreditsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, CreditsAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
            CreditsAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "CreditsWrite")
            {
                context.Succeed(requirement);
                return noOp;
            }

            if (requirement.Name == "CreditsDelete")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    if (resource.Credits.TotalCredits >= 5)
                    {
                        context.Succeed(requirement);
                        return noOp;
                    }
                }
            }

            if (requirement.Name == "CreditsRead")
            {
                context.Succeed(requirement);
                return noOp;
            }

            return noOp;
        }
    }
}