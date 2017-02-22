using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.CommentAggregate.InMemory
{
    public static class InMemoryComments
    {
        public static List<Comment> Comments = new List<Comment>();
    }
}