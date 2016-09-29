using System;
using TFN.Api.Authorization.Models.Resource.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Authorization.Models.Resource
{
    public class ScoreAuthorizationModel : ScoreAuthorizationModel<Guid>
    {
        private ScoreAuthorizationModel(Guid ownerId, Guid resourceId, Guid commentId)
            : base(ownerId, resourceId, commentId)
        {
            
        }

        public static ScoreAuthorizationModel From(Score resource, Guid commentOwnerId)
        {
            return new ScoreAuthorizationModel(resource.UserId,resource.Id, commentOwnerId);
        }
    }
    public class ScoreAuthorizationModel<TKey> : OwnedResource<TKey, TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey CommentOwnerId { get; private set; }
        public ScoreAuthorizationModel(TKey ownerId, TKey resourceId, TKey commentOwnerId)
            : base(ownerId, resourceId)
        {
            CommentOwnerId = commentOwnerId;
        }
    }
}
