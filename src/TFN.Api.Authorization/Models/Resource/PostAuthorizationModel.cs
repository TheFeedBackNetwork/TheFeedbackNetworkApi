using System;
using TFN.Api.Authorization.Models.Resource.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Authorization.Models.Resource
{
    public class PostAuthorizationModel : PostAuthorizationModel<Guid>
    {
        private PostAuthorizationModel(Guid ownerId, Guid postId)
            : base(ownerId, postId)
        {
            
        }

        public static PostAuthorizationModel From(Post resource)
        {
            return new PostAuthorizationModel(resource.UserId,resource.Id);
        }
    }
    public class PostAuthorizationModel<TKey> : OwnedResource<TKey, TKey>
        where TKey : IEquatable<TKey>
    {
        public PostAuthorizationModel(TKey ownerId, TKey resourceId)
            : base(ownerId, resourceId)
        {

        }
    }
}
