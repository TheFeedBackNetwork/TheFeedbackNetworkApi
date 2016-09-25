using System.Collections.Generic;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public static class InMemoryUsers
    {
        public static List<User> Users = new List<User>
        {
            new User("testuser1", "http://blah.com/img.jpg", "test@email.com", "testName", "testFamilyName",
                new Biography("", "", "", "")),
            new User("testuser2", "http://blah2.com/img.jpg", "test2@email.com", "testName2", "testFamilyName2",
                new Biography("", "", "", "")),
        };
    }
}
