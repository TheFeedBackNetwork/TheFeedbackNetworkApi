using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Event;

namespace TFN.ActorSystem.Actors.PostsSystem
{
    public class PostsSystemActor : ReceiveActor
    {
        private ILoggingAdapter Logger;
        private List<DateTime> Time;
        public PostsSystemActor(ILoggingAdapter logger)
        {
            Logger = logger;
        }

        public PostsSystemActor()
        {
            
        }
        protected override void PreStart()
        {
            Time = new List<DateTime>();
            Become(Ready);
        }

        public void Ready()
        {
            Receive<PostsSystemMessages.Tap>(message =>
            {
                Time.Add(DateTime.UtcNow);
            });

            Receive<PostsSystemMessages.GetTime>(message =>
            {
                Sender.Tell(new PostsSystemMessages.Time {TimeNow = Time.LastOrDefault(), Count = Time.Count});
            });
        }
    }
}