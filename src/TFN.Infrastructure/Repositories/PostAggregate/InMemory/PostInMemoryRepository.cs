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

            return Task.CompletedTask;
        }

        public Task AddAsync(Comment entity)
        {
            if(InMemoryPosts.Posts.Any(x => x.Id == entity.PostId))
            {
                InMemoryComments.Comments.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task AddAsync(Score entity)
        {
            if(InMemoryComments.Comments.Any(x => x.Id == entity.CommentId))
            {
                InMemoryScores.Scores.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Post>> GetAllAsync(int postOffset, int postLimit)
        {
            IReadOnlyList<Post> posts = InMemoryPosts.Posts.Skip(postOffset).Take(postLimit).Where(x => x.IsActive).ToList();

            return Task.FromResult(posts);
        }

        public Task<Post> GetAsync(Guid postId)
        {
            return Task.FromResult(InMemoryPosts.Posts.SingleOrDefault(x => x.Id == postId && x.IsActive));
        }

        public Task<IReadOnlyList<Comment>> GetCommentsAsync(Guid postId, int commentOffset, int commentLimit)
        {
            IReadOnlyList<Comment> comments =
                InMemoryComments.Comments.FindAll(x => x.PostId == postId)
                    .OrderBy(x => x.Created)
                    .Skip(commentOffset)
                    .Take(commentLimit)
                    .ToList();

            return Task.FromResult(comments);
        }

        public Task<Comment> GetAsync(Guid postId, Guid commentId)
        {
            var comment = InMemoryComments.Comments.SingleOrDefault(x => x.PostId == postId && x.Id == commentId);

            return Task.FromResult(comment);
        }

        public Task<Score> GetAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            if(InMemoryPosts.Posts.Any(x => x.Id == postId))
            {
                var score = InMemoryScores.Scores.SingleOrDefault(x => x.CommentId == commentId && x.Id == scoreId);
                return Task.FromResult(score);
            }

            return Task.FromResult<Score>(null);
        }

        public Task UpdateAsync(Comment entity)
        {
            InMemoryComments.Comments.RemoveAll(x => x.Id == entity.Id);
            InMemoryComments.Comments.Add(entity);

            return Task.CompletedTask;
        }
        public Task UpdateAsync(Post entity)
        {
            DeleteAsync(entity.Id);
            AddAsync(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            InMemoryPosts.Posts.RemoveAll(x => x.Id == id);

            return Task.CompletedTask;
        }


        public Task DeleteAsync(Guid postId, Guid commentId)
        {
            InMemoryComments.Comments.RemoveAll(x => x.PostId == postId && x.Id == commentId);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            if (InMemoryPosts.Posts.Any(x => x.Id == postId))
            {
                InMemoryScores.Scores.RemoveAll(x => x.CommentId == commentId && scoreId == x.Id);
            }

            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Comment>> GetAllCommentsAsync(Guid postId)
        {
            IReadOnlyList<Comment> comments = InMemoryComments.Comments.Where(x => x.PostId == postId).ToList();
            return Task.FromResult(comments);
        }

        public Task<IReadOnlyList<Score>> GetAllScoresAsync(Guid postId, Guid commentId)
        {
            if(InMemoryPosts.Posts.Any(x => x.Id == postId))
            {
                IReadOnlyList<Score> scores = InMemoryScores.Scores.Where(x => x.CommentId == commentId).ToList();
                return Task.FromResult(scores);
            }

            return Task.FromResult<IReadOnlyList<Score>>(null);
        }
    }
}
