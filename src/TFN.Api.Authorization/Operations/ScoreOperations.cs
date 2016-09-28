using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Api.Authorization.Operations
{
    public static class ScoreOperations
    {
        public static OperationAuthorizationRequirement Write = new OperationAuthorizationRequirement { Name = "ScoreWrite" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "ScoreDelete" };
    }
}
