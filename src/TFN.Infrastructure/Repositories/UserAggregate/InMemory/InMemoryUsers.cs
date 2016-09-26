using System;
using System.Collections.Generic;
using NodaTime;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public static class InMemoryUsers
    {
        public static List<User> Users = new List<User>
        {
            User.Hydrate(new Guid("f42c8b85-c058-47cb-b504-57f750294469"),"bob", "blah.com/img.jpg", "bob@email.com", "Bob", "McFoo",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/foo", "http://soundcloud.com/foo", ""),Instant.FromUtc(2016,6,6,6,6), true),
            User.Hydrate(new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"),"alice", "alice.com/img.jpg", "alice@email.com", "Alice", "McBar",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/bar", "http://soundcloud.com/bar", ""),Instant.FromUtc(2016,6,6,6,6), true),

        };
    }
}
