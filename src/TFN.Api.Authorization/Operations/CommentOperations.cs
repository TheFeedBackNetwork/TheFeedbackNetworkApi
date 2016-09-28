using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Api.Authorization.Operations
{
    public static class CommentOperations
    {
        public static OperationAuthorizationRequirement Write = new OperationAuthorizationRequirement { Name = "CommentWrite" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "CommentDelete" };
        public static OperationAuthorizationRequirement Edit = new OperationAuthorizationRequirement { Name = "CommentEdit" };
    }
}
