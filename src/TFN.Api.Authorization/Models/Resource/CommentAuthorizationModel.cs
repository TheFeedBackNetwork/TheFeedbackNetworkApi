using System;
using TFN.Api.Authorization.Models.Resource.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Authorization.Models.Resource
{
    public class CommentAuthorizationModel : CommentAuthorizationModel<Guid>
    {
        private CommentAuthorizationModel(Guid ownerId, Guid resourceId, Guid postId)
            : base(ownerId, resourceId, postId)
        {
            
        }

        public static CommentAuthorizationModel From(Comment resource)
        {
            return new CommentAuthorizationModel(resource.UserId,resource.Id,resource.PostId);
        }
    }
    public class CommentAuthorizationModel<TKey> : OwnedResource<TKey, TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey PostId { get; private set; }
        public CommentAuthorizationModel(TKey ownerId, TKey resourceId, TKey postId)
            :base(ownerId,resourceId)
        {
            PostId = postId;
        }
    }
}
