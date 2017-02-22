using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Infrastructure.Repositories.CommentAggregate.InMemory
{
    public class CommentInMemoryRepository : ICommentRepository
    {
        public Task AddAsync(Comment entity)
        {
            InMemoryComments.Comments.Add(entity);

            return Task.CompletedTask;
        }

        public Task AddAsync(Score entity)
        {
            if (InMemoryComments.Comments.Any(x => x.Id == entity.CommentId))
            {
                InMemoryScores.Scores.Add(entity);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            InMemoryComments.Comments.RemoveAll(x => x.Id == id);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid commentId, Guid scoreId)
        {
            InMemoryScores.Scores.RemoveAll(x => x.CommentId == commentId && scoreId == x.Id);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Comment>> GetAllCommentsAsync(Guid postId)
        {
            IReadOnlyList<Comment> comments = InMemoryComments.Comments.Where(x => x.PostId == postId).ToList();
            return Task.FromResult(comments);
        }

        public Task<IReadOnlyList<Score>> GetAllScores(Guid commentId, int offset, int limit)
        {
            IReadOnlyList<Score> scores = InMemoryScores.Scores.FindAll(x => x.CommentId == commentId)
                    .OrderBy(x => x.Created)
                    .Skip(offset)
                    .Take(limit)
                    .ToList();

            return Task.FromResult(scores);
        }

        public Task<Comment> GetAsync(Guid id)
        {
            var comment = InMemoryComments.Comments.SingleOrDefault(x => x.Id == id);

            return Task.FromResult(comment);
        }

        public Task<Score> GetAsync(Guid commentId, Guid scoreId)
        {
            var score = InMemoryScores.Scores.SingleOrDefault(x => x.CommentId == commentId && x.Id == scoreId);
            return Task.FromResult(score);
        }

        public Task<IReadOnlyList<Comment>> GetCommentsAsync(Guid postId, int offset, int limit)
        {
            IReadOnlyList<Comment> comments =
                InMemoryComments.Comments.FindAll(x => x.PostId == postId)
                    .OrderBy(x => x.Created)
                    .Skip(offset)
                    .Take(limit)
                    .ToList();

            return Task.FromResult(comments);
        }

        public Task<CommentSummary> GetCommentScoreSummaryAsync(Guid commentId, int limit, string username)
        {
            var hasScored = InMemoryScores.Scores.Any(x => x.Username == username && x.CommentId == commentId);
            var count = InMemoryScores.Scores.FindAll(x => x.CommentId == commentId).Count;
            IReadOnlyList<Score> someScores = InMemoryScores.Scores.FindAll(x => x.CommentId == commentId).Take(limit).ToList();

            var summary = CommentSummary.From(commentId,someScores,count,hasScored);

            return Task.FromResult(summary);
        }

        public Task UpdateAsync(Comment entity)
        {
            InMemoryComments.Comments.RemoveAll(x => x.Id == entity.Id);
            InMemoryComments.Comments.Add(entity);

            return Task.CompletedTask;
        }
    }
}