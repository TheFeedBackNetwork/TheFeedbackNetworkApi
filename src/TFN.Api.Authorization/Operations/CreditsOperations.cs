using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Api.Authorization.Operations
{
    public class CreditsOperations
    {
        public static OperationAuthorizationRequirement Write = new OperationAuthorizationRequirement { Name = "CreditsWrite" };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "CreditsDelete" };
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = "CreditsRead" };
    }
}