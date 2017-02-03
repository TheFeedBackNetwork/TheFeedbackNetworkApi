using Akka.Actor;
using TFN.ActorSystem.Actors.SignalRBridge;
using TFN.ActorSystem.Actors.User;
using TFN.ActorSystem.Actors.UserCoordinator;
using TFN.Domain.Interfaces.Services;

namespace TFN.ActorSystem.Actors.UsersSystem
{
    public class UsersSystemActor : ReceiveActor
    {
        public IActorRef UserCoordinator { get; private set; }
        public IUsersEventsService UsersEventsService { get; private set; }
        public UsersSystemActor()
        {
            Become(Ready);
        }

        protected override void PreStart()
        {
            base.PreStart();

            UserCoordinator = Context.ActorOf(Props.Create(() => new UserCoordinatorActor(UsersEventsService)));
        }

        public void Ready()
        {
            Receive<UsersSystemMessages.StartSession>(message =>
            {
                UserCoordinator.Tell(message,Sender);
            });

            Receive<UsersSystemMessages.EndSession>(message =>
            {
                UserCoordinator.Tell(message, Sender);
            });

            Receive<UserMessages.UploadProgress>(message =>
            {
                UserCoordinator.Forward(message);
            });

            Receive<UserMessages.ProcessProgress>(message =>
            {
                UserCoordinator.Forward(message);
            });

            Receive<SignalRBridgeMessages.GetUserCoordinatorRef>(message =>
            {
                Sender.Tell(new SignalRBridgeMessages.GetUserCoordinatorRef(), UserCoordinator);
            });
        }        
    }
}
