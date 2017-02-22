using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.PostAggregate.InMemory
{
    public static class InMemoryLikes
    {
        public static List<Like> Likes = new List<Like>();
    }
}