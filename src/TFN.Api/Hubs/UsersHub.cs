using System;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.SignalR;
using TFN.ActorSystem;
using TFN.ActorSystem.Actors.UsersSystem;

namespace TFN.Api.Hubs
{
    public class UsersHub : AppHub
    {
        [Authorize]
        public override Task OnConnected()
        {
            StartSession(UserId);

            return base.OnConnected();
        }

        [Authorize]
        public override Task OnReconnected()
        {  
            StartSession(UserId);

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                EndSession(UserId);
                // We know that Stop() was called on the client,
                // and the connection shut down gracefully.
            }
            else
            {
                EndSession(UserId);
                // This server hasn't heard from the client in the last ~35 seconds.
                // If SignalR is behind a load balancer with scaleout configured, 
                // the client may still be connected to another SignalR server.
            }

            return base.OnDisconnected(stopCalled);
        }

        private void StartSession(Guid userId)
        {    
            var startSessionMessage = new UsersSystemMessages.StartSession(userId);
            SystemActors.UserSystemActor.Tell(startSessionMessage, ActorRefs.NoSender);
        }

        private void EndSession(Guid userId)
        {
            var endSessionMessage = new UsersSystemMessages.EndSession(userId);
            SystemActors.UserSystemActor.Tell(endSessionMessage, ActorRefs.NoSender);
        }


    }
}