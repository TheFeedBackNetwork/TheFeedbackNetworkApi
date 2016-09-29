using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TFN.Api.Authorization.Models.Resource;

namespace TFN.Api.Authorization.Handlers
{
    public class CommentAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, CommentAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, CommentAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "CommentWrite")
            {
                context.Succeed(requirement);
                return noOp;
            }

            if (requirement.Name == "CommentDelete")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            if (requirement.Name == "CommentEdit")
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
