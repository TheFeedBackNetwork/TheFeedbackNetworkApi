using System;

namespace TFN.Api.Authorization.Models.Resource.Base
{
    public abstract class OwnedResource<TOwnerIdentity, TResourceIdentity>
        where TOwnerIdentity : IEquatable<TOwnerIdentity>
        where TResourceIdentity : IEquatable<TOwnerIdentity>
    {
        private TOwnerIdentity ownerId;
        private TResourceIdentity resourceId;

        public TOwnerIdentity OwnerId
        {
            get
            {
                return ownerId;
            }
        }

        public TResourceIdentity ResourceId
        {
            get
            {
                return resourceId;;
            }
        }

        protected OwnedResource(TOwnerIdentity ownerIdentity, TResourceIdentity resourceIdentity)
        {
            if (ownerIdentity.Equals(default(TOwnerIdentity)))
            {
                throw new ArgumentOutOfRangeException(nameof(ownerIdentity), "The identifier cannot be equal to the default value of the type.");
            }

            if (resourceIdentity.Equals(default(TResourceIdentity)))
            {
                throw new ArgumentOutOfRangeException(nameof(resourceIdentity), "The identifier cannot be equal to the default value of the type.");
            }
            resourceId = resourceIdentity;
            ownerId = ownerIdentity;
        }

        protected bool HasSameOwner(OwnedResource<TOwnerIdentity, TResourceIdentity> otherResource)
        {
            if(otherResource == null)
            {
                return false;
            }

            return OwnerId.Equals(otherResource.OwnerId);
        }

        protected bool HasSameResourceIdentifier(TResourceIdentity otherResourceIdentity)
        {
            if (otherResourceIdentity == null)
            {
                return false;
            }

            return ResourceId.Equals(otherResourceIdentity);
        }
    }
}
