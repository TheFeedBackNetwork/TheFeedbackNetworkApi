using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TransientUserAggregate.InMemory
{
    public static class InMemoryTransientUsers
    {
        public static List<TransientUser> TransientUsers = new List<TransientUser>();
    }
}