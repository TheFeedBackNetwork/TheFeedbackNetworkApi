using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TFN.Api.Authorization.Models.Resource;

namespace TFN.Api.Authorization.Handlers
{
    public class ScoreAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ScoreAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ScoreAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "ScoreWrite")
            {
                if (context.User.HasClaim("sub", resource.CommentOwnerId.ToString()))
                {
                    return noOp;
                }
            }

            if (requirement.Name == "ScoreDelete")
            {
                if (context.User.HasClaim("sub", resource.CommentOwnerId.ToString()))
                {
                    return noOp;
                }
            }

            context.Succeed(requirement);
            return noOp;
        }
    }
}
