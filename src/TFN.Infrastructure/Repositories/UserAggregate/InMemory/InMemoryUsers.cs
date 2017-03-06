using System;
using System.Collections.Generic;
using NodaTime;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public static class InMemoryUsers
    {
        //pass lol123
        public static List<User> Users = new List<User>
        {
            User.Hydrate(new Guid("f42c8b85-c058-47cb-b504-57f750294469"),"bob", "https://i1.sndcdn.com/artworks-000210686002-378biv-t500x500.jpg", "bob@email.com", "Bob",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/foo", "http://soundcloud.com/foo","http://twitter.com/foo","","http://facebook.com/foo","Lorem IIIIPPSSS"),Instant.FromUtc(2016,6,6,6,6), true),
            User.Hydrate(new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"),"alice", "https://i1.sndcdn.com/artworks-000189817033-ocnqxs-t500x500.jpg", "alice@email.com", "Alice",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/bar", "http://soundcloud.com/bar","http://twitter.com/foo","http://youtube.com/foo","http://facebook.com/foo","Lorem IIIIPPSSS"),Instant.FromUtc(2016,6,6,6,6), true),
            User.Hydrate(new Guid("b984088b-bbab-4e3e-9a40-c07238475cb7"),"lutz", "https://i1.sndcdn.com/avatars-000282654916-bcj25r-t500x500.jpg", "lutando@ngqakaza.com", "Lutz",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/baz", "http://soundcloud.com/baz","http://twitter.com/foo","http://youtube.com/foo","http://facebook.com/foo","Lorem IIIIPPSSS"),Instant.FromUtc(2016,6,6,6,6), true),

        };
    }
}
