using System;
using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.CreditAggregate.InMemory
{
    public static class InMemoryCredits
    {
        public static List<Credits> Credits = new List<Credits>
        {
            Domain.Models.Entities.Credits.Hydrate(new Guid("d796bb5f-bcf0-4430-adce-cf9c0bcb45f3"),
                new Guid("f42c8b85-c058-47cb-b504-57f750294469"), "bob", 10),
            Domain.Models.Entities.Credits.Hydrate(new Guid("d4c9d75b-c17b-4040-b647-6e4419ca670d"),
                new Guid("3f9969b7-c267-4fc5-bedf-b05d211ba1d6"), "alice", 5),
            Domain.Models.Entities.Credits.Hydrate(new Guid("8809eb82-2226-4e6e-a982-b2b9db0ce054"),
                new Guid("b984088b-bbab-4e3e-9a40-c07238475cb7"), "lutz", 50),
        };
    }
}