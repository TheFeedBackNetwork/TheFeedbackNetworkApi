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
        public  Task AddAsync(Post entity)
        {
            InMemoryPosts.Posts.Add(entity);
            return Task.FromResult(0);
        }


        public Task DeleteAsync(Guid id)
        {
            InMemoryPosts.Posts.RemoveAll(x => x.Id == id);
            return Task.FromResult(0);
        }

        public Task<IReadOnlyList<Post>> GetAllAsync(int postOffset, int postLimit, int commentOffset, int commentLimit)
        {
            IReadOnlyList<Post> posts = InMemoryPosts.Posts.Skip(postOffset).Take(postLimit).ToList();
            return Task.FromResult(posts);
        }

        public Task<Post> GetAsync(Guid id)
        {
            return Task.FromResult(InMemoryPosts.Posts.SingleOrDefault(x => x.Id == id));
        }

        public Task<Post> GetAsync(Guid id, int commentOffset, int commentLimit)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Post entity)
        {
            DeleteAsync(entity.Id);
            AddAsync(entity);
            return Task.FromResult(0);
        }
    }
}
