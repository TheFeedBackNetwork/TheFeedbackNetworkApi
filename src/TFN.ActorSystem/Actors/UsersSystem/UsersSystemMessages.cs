using System;

namespace TFN.ActorSystem.Actors.UsersSystem
{
    public class UsersSystemMessages
    {
        public class StartSession
        {
            public Guid UserId { get; private set; }

            public StartSession(Guid userId)
            {
                UserId = userId;
            }
        }

        public class EndSession
        {
            public Guid UserId { get; private set; }

            public EndSession(Guid userId)
            {
                UserId = userId;
            }
        }
    }
}