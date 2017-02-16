using Akka.Actor;
using TFN.ActorSystem.Actors.User;
using TFN.Domain.Interfaces.Services;

namespace TFN.ActorSystem.Actors.SignalRBridge
{
    public class SignalRBridgeActor : ReceiveActor
    {
        public IUsersEventsService UsersEventsService { get; private set; }
        public IActorRef UserCoordinator { get; private set; }
        public SignalRBridgeActor(IUsersEventsService usersEventsService)
        {
            UsersEventsService = usersEventsService;          
            SystemActors.UserSystemActor.Tell(new SignalRBridgeMessages.GetUserCoordinatorRef());

            Become(Waiting);
        }

        public void Waiting()
        {
            Receive<SignalRBridgeMessages.GetUserCoordinatorRef>(message =>
            {
                UserCoordinator = Sender;
                Become(Ready);
            });
        }

        public void Ready()
        {
            Receive<UserMessages.UploadProgress>(message =>
            {
                UsersEventsService.UploadProgress(message.UserId.ToString(), message.Progress);
            });

            Receive<UserMessages.ProcessProgress>(message =>
            {
                UsersEventsService.ProcessingProgress(message.UserId.ToString(), message.Progress);
            });
        }
    }
}