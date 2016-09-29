using System;
using System.Collections.Generic;
using NodaTime;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.Enums;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Infrastructure.Repositories.PostAggregate.InMemory
{
    public static class InMemoryPosts
    {
        public static List<Post> Posts = new List<Post>();
        /*public static List<Post> Posts = new List<Post>
        {
            Post.Hydrate(new Guid("6f7081f6-9072-4a51-ae0c-7d4c0041b45a"),new Guid("f42c8b85-c058-47cb-b504-57f750294469"),"bob","http://soundcloud.com/foo/bar","Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                0,Genre.Dubstep,new List<string> {"dub","drop"},new List<Comment>(),true,Instant.FromUtc(2016,6,6,7,7), Instant.FromUtc(2016,6,6,7,8)  ),
            Post.Hydrate(
                new Guid("abb8eab7-a40d-4328-a352-939681e82a13"),new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"),"alice","http://soundcloud.com/bar/foo","Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                0,Genre.Trance,
                new List<string> {"goa","asot"},
                new List<Comment> {Comment.Hydrate(new Guid("fe6d0fb3-109c-4283-8032-71bfbab8bd0a"),new Guid("f42c8b85-c058-47cb-b504-57f750294469"),new Guid("abb8eab7-a40d-4328-a352-939681e82a13"),"bob","Hey nice one",new List<Score> {new Score(new Guid("fe6d0fb3-109c-4283-8032-71bfbab8bd0a"), new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"),"alice")},true,Instant.FromUtc(2016,8,8,8,8),Instant.FromUtc(2016,8,8,8,8))},
                true,
                Instant.FromUtc(2016,8,8,8,8), Instant.FromUtc(2016,8,8,8,8)) 

        };*/
    }
}