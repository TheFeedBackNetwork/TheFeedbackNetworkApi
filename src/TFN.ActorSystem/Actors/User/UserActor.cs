using Akka.Actor;
using TFN.ActorSystem.Actors.UsersSystem;

namespace TFN.ActorSystem.Actors.User
{
    public class UserActor : ReceiveActor
    {
        public string UserId { get; private set; }

        public UserActor(string userId)
        {
            UserId = userId;
            Become(Ready);
        }

        public void Ready()
        {
            Receive<UsersSystemMessages.EndSession>(message =>
            {
                var kys = PoisonPill.Instance;
                Self.Tell(kys);
            });
        }
    }
}
