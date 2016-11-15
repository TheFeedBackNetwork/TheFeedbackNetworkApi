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
        
    }
}