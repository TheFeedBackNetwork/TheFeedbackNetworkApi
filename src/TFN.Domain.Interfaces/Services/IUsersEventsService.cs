namespace TFN.Domain.Interfaces.Services
{
    public interface IUsersEventsService
    {
        void UploadProgress(string userId, int progress);

        void ProcessingProgress(string userid, int progress);
    }
}