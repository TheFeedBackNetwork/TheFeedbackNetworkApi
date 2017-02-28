using System;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class CommentResponseModel : ResourceResponseModel
    {
        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public string Username { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        [Excludable]
        public CommentSummaryResponseModel CommentSummary { get; private set; }
        [Excludable]
        public ResourceAuthorizationResponseModel ResourceAuthorization { get; private set; }

        private CommentResponseModel(Guid id, Guid postId, Guid userId, string text, string username, DateTime created, DateTime modified,CommentSummaryResponseModel summary,ResourceAuthorizationResponseModel authZ, string apiUrl)
            : base(GetHref(id,apiUrl),id)
        {
            PostId = postId;
            Text = text;
            Username = username;
            UserId = userId;
            Created = created;
            Modified = modified;
            CommentSummary = summary;
            ResourceAuthorization = authZ;
        }

        private static Uri GetHref(Guid commentId, string apiUrl)
        {
            return new Uri($"{apiUrl}/comments/{commentId}");
        }

        internal static CommentResponseModel From(Comment comment,CommentSummary summary,Credits credits,ResourceAuthorizationResponseModel authZ, string apiUrl)
        {
            string postApiurl = new Uri($"{apiUrl}/api/posts/{comment.PostId}").AbsoluteUri;
            return new CommentResponseModel(
                comment.Id,
                comment.PostId,
                comment.UserId,
                comment.Text,
                comment.Username,
                comment.Created.ToDateTimeUtc(),
                comment.Modified.ToDateTimeUtc(),
                CommentSummaryResponseModel.From(summary,credits,apiUrl,comment.PostId), 
                authZ,
                postApiurl
                );
        }
    }
}