using System;

namespace TFN.ActorSystem.Actors.User
{
    public class UserMessages
    {
        public class UploadProgress
        {
            public int Progress { get; private set; }
            public Guid UserId { get; private set; }

            public UploadProgress(Guid userId, int progress)
            {
                Progress = progress;
                UserId = userId;
            }
        }

        public class ProcessProgress
        {
            public int Progress { get; private set; }
            public Guid UserId { get; private set; }

            public ProcessProgress(Guid userId, int progress)
            {
                Progress = progress;
                UserId = userId;

            }
        }
    }
}