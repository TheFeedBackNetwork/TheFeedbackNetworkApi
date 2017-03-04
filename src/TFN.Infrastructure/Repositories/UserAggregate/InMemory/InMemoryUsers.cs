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
            User.Hydrate(new Guid("f42c8b85-c058-47cb-b504-57f750294469"),"bob", "https://thumb1.shutterstock.com/display_pic_with_logo/64260/371570986/stock-photo-smiling-man-with-crossed-arms-over-gray-background-371570986.jpg", "bob@email.com", "Bob",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/foo", "http://soundcloud.com/foo","http://twitter.com/foo","http://youtube.com/foo","http://facebook.com/foo","Lorem IIIIPPSSS"),Instant.FromUtc(2016,6,6,6,6), true),
            User.Hydrate(new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"),"alice", "https://thumb1.shutterstock.com/display_pic_with_logo/799939/328676510/stock-photo-portrait-of-cute-beautiful-young-girl-with-freckles-close-up-328676510.jpg", "alice@email.com", "Alice",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/bar", "http://soundcloud.com/bar","http://twitter.com/foo","http://youtube.com/foo","http://facebook.com/foo","Lorem IIIIPPSSS"),Instant.FromUtc(2016,6,6,6,6), true),
            User.Hydrate(new Guid("b984088b-bbab-4e3e-9a40-c07238475cb7"),"lutz", "https://thumb1.shutterstock.com/display_pic_with_logo/1595666/415033600/stock-photo-portrait-of-a-mature-black-man-smiling-at-home-415033600.jpg", "lutando@ngqakaza.com", "Lutz",
                new Biography("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    "http://instagram.com/baz", "http://soundcloud.com/baz","http://twitter.com/foo","http://youtube.com/foo","http://facebook.com/foo","Lorem IIIIPPSSS"),Instant.FromUtc(2016,6,6,6,6), true),

        };
    }
}
