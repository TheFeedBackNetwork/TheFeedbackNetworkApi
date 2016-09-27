using System;
using System.Collections.Generic;
using System.Linq;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class CommentResponseModel : ResourceResponseModel
    {

        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public string Username { get; private set; }
        public Guid UserId { get; private set; }
        [Excludable]
        public IReadOnlyList<ScoreResponseModel> Scores { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        private CommentResponseModel(Guid id, Guid postId, Guid userId, string text, string username,
            IReadOnlyList<ScoreResponseModel> scores, DateTime created, DateTime modified, string apiUrl)
            : base(GetHref(id,apiUrl),id)
        {
            PostId = postId;
            Text = text;
            Username = username;
            UserId = userId;
            Scores = scores;
            Created = created;
            Modified = modified;
        }

        private static Uri GetHref(Guid commentId, string apiUrl)
        {
            return new Uri($"{apiUrl}/comments/{commentId}");
        }

        internal static CommentResponseModel From(Comment comment, string apiUrl)
        {
            string postApiurl = GetHref(comment.Id, apiUrl).AbsoluteUri;
            return new CommentResponseModel(
                comment.Id,
                comment.PostId,
                comment.UserId,
                comment.Text,
                comment.Username,
                comment.Scores.Select(x => ScoreResponseModel.From(x,postApiurl)).ToList(),
                comment.Created.ToDateTimeUtc(),
                comment.Modified.ToDateTimeUtc(),
                apiUrl
                );
        }
    }
}