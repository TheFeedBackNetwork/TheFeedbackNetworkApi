using Akka.Actor;
using TFN.ActorSystem.Actors.User;
using TFN.ActorSystem.Actors.UsersSystem;
using TFN.Domain.Interfaces.Services;

namespace TFN.ActorSystem.Actors.UserCoordinator
{
    public class UserCoordinatorActor : ReceiveActor
    {
        public UserCoordinatorActor(IUsersEventsService usersEventsService)
        {
            
        }
        public void Ready()
        {
            Receive<UsersSystemMessages.StartSession>(message =>
            {
                var userActor = CreateIfNotExist(message.UserId.ToString());
            });

            Receive<UsersSystemMessages.EndSession>(message =>
            {
                var userActor = CreateIfNotExist(message.UserId.ToString());
                userActor.Tell(message);
            });
        }

        private IActorRef CreateIfNotExist(string userId)
        {
            var userActor = Context.Child(userId);

            if (userActor.IsNobody())
            {
                userActor = Context.ActorOf(Props.Create(() => new UserActor(userId)), userId);
            }

            return userActor;
        }
    }


}
