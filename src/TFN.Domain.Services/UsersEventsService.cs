using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using TFN.Domain.Interfaces.Services;

namespace TFN.Domain.Services
{
    public class UsersEventsService : IUsersEventsService
    {
        public IHubContext HubContext { get; private set; }
        public UsersEventsService(IConnectionManager connectionManager)
        {
            HubContext = connectionManager.GetHubContext("UsersHub");
        }

        public void UploadProgress(string userId,int progress)
        {
            HubContext.Clients.User(userId).uploadProgress(progress);
        }

        public void ProcessingProgress(string userId, int progress)
        {
            HubContext.Clients.User(userId).processingProgress(progress);
        }
    }
}