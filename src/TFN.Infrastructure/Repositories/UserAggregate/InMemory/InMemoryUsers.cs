using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public static class InMemoryUsers
    {
        public static List<User> Users = new List<User>();
    }
}
