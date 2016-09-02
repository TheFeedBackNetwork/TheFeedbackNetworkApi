using System;
using NodaTime;

namespace TFN.Domain.Models.ValueObjects
{
    public class Score
    {
        public Guid UserId { get; private set; }
        public Instant Created { get; private set; }

        private Score(Guid userId, Instant created)
        {
            UserId = userId;
            Created = created;
        }

        public static Score From(Guid userId, Instant created)
        {
            return new Score(userId, created);
        }
    }
}
