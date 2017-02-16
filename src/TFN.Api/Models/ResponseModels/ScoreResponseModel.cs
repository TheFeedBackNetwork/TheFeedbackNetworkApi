using System;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class ScoreResponseModel : ResourceResponseModel
    {
        public Guid CommentId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public DateTime Created { get; private set; }

        private ScoreResponseModel(Guid id,Guid commentId, Guid userId,string username, DateTime created, string apiUrl)
            : base(GetHref(commentId,id,apiUrl),id)
        {
            CommentId = commentId;
            UserId = userId;
            Username = username;
            Created = created;
        }
        private static Uri GetHref( Guid commentId,Guid scoreId, string apiUrl)
        {
            return new Uri($"{apiUrl}/comments/{commentId}/scores/{scoreId}");
        }

        public static ScoreResponseModel From(Score score, string apiUrl, Guid postId)
        {
            string postApiurl = new Uri($"{apiUrl}/api/posts/{postId}").AbsoluteUri;

            return new ScoreResponseModel(
                score.Id,
                score.CommentId,
                score.UserId,
                score.Username,
                score.Created.ToDateTimeUtc(),
                postApiurl
                );
        }
    }
}
