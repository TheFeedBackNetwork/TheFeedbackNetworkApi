using System;
using TFN.Api.Authorization.Models.Resource.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Authorization.Models.Resource
{
    public class TrackAuthorizationModel : TrackAuthorizationModel<Guid>
    {
        private TrackAuthorizationModel(Guid ownerId, Guid postId)
            : base(ownerId, postId)
        {
            
        }

        public static TrackAuthorizationModel From(Track resource)
        {
            return new TrackAuthorizationModel(resource.UserId, resource.Id);
        }
    }

    public class TrackAuthorizationModel<TKey> : OwnedResource<TKey, TKey>
        where TKey : IEquatable<TKey>
    {
        public TrackAuthorizationModel(TKey ownerId, TKey resourceId)
            : base(ownerId, resourceId)
        {
            
        }
    } 
}