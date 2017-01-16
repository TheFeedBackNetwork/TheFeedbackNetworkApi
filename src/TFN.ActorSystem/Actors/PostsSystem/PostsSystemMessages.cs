using System;

namespace TFN.ActorSystem.Actors.PostsSystem
{
    public class PostsSystemMessages
    {
        public class Tap { }

        public class GetTime { }

        public class Time
        {
            public DateTime TimeNow { get; set; }
            public int Count { get; set; }
        }
    }
}