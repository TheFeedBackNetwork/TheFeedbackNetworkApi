using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TFN.Api.Authorization.Models.Resource;

namespace TFN.Api.Authorization.Handlers
{
    public class PostAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PostAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, PostAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "PostWrite")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            if (requirement.Name == "PostDelete")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            if (requirement.Name == "PostEdit")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            return noOp;
        }
    }
}
