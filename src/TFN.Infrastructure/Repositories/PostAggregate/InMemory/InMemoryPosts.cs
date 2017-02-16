using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.PostAggregate.InMemory
{
    public static class InMemoryPosts
    {
        public static List<Post> Posts = new List<Post>();      
    }
}