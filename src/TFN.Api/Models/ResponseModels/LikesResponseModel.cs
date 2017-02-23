using System;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class LikesResponseModel : ResourceResponseModel
    {
        public Guid PostId { get; private set; }
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public DateTime Created { get; private set; }

        private LikesResponseModel(Guid id, Guid postId, Guid userId, string username, DateTime created, string apiUrl)
            : base(GetHref(postId,id,apiUrl),id)
        {
            PostId = postId;
            UserId = userId;
            Username = username;
            Created = created;
        }

        private static Uri GetHref(Guid postId, Guid likeId,string apiUrl)
        {
            return new Uri($"{apiUrl}/api/posts/{postId}/likes/likeId");
        }

        public static LikesResponseModel From(Like like, string apiUrl)
        {
            return new LikesResponseModel(
                like.Id,
                like.PostId,
                like.UserId,
                like.Username,
                like.Created.ToDateTimeUtc(),
                apiUrl
                );
        }
    }
}