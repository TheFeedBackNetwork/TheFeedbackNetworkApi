using System;
using TFN.Api.Authorization.Models.Resource.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Authorization.Models.Resource
{
    public class CreditsAuthorizationModel : CreditsAuthorizationModel<Guid>
    {
        public Credits Credits { get; private set; }
        private CreditsAuthorizationModel(Guid ownerId, Guid creditsId, Credits credits)
            : base(ownerId, creditsId)
        {
            Credits = credits;
        }

        public static CreditsAuthorizationModel From(Credits resource)
        {
            return new CreditsAuthorizationModel(resource.UserId,resource.Id, resource);
        }
    }

    public class CreditsAuthorizationModel<TKey> : OwnedResource<TKey, TKey>
        where TKey : IEquatable<TKey>
    {
        public CreditsAuthorizationModel(TKey ownerId, TKey resourceId)
            : base(ownerId, resourceId)
        {
            
        }
    } 
}