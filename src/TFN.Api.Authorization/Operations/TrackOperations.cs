using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Api.Authorization.Operations
{
    public static class TrackOperations
    {
        public static OperationAuthorizationRequirement Write = new OperationAuthorizationRequirement { Name = "TrackWrite" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "TrackDelete" };
    }
}