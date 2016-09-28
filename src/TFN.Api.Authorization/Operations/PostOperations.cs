using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Api.Authorization.Operations
{
    public static class PostOperations
    {
        public static OperationAuthorizationRequirement Write = new OperationAuthorizationRequirement {Name = "PostWrite"};
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement {Name = "PostDelete" };
        public static OperationAuthorizationRequirement Edit = new OperationAuthorizationRequirement {Name = "PostEdit" };

    }

}
