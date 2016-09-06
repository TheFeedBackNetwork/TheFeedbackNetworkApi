using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.PostAggregate.InMemory
{
    public class PostInMemoryRepository : IPostRepository
    {
        public Task AddAsync(Post entity)
        {
            throw new NotImplementedException();
        }

        public Task AddBulkAsync(IEnumerable<Post> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Post FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
