using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TFN.Api.Authorization.Models.Resource;

namespace TFN.Api.Authorization.Handlers
{
    public class TrackAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, TrackAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TrackAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "TrackWrite")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            if (requirement.Name == "TrackDelete")
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