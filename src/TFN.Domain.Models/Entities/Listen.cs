using System;
using NodaTime;
using TFN.Domain.Models.Enums;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Listen : DomainEntity<Guid>
    {
        public Guid PostId { get; private set; }
        public Listener Listener { get; private set; }
        public string Username { get; private set; }
        public string IPAddress { get; private set; }
        
        public Instant Created { get; private set; }

        private Listen(Guid id, Guid postId, Listener listener, string username, string ipAddress, Instant created)
            : base(id)
        {
            PostId = postId;
            Username = username;
            Listener = listener;
            IPAddress = ipAddress;
            Created = created;
        }

        public Listen(Guid postId,Listener listener, string username, string ipAddress)
            : this(Guid.NewGuid(), postId,listener,username,ipAddress,SystemClock.Instance.Now)
        {
            
        }

        public static Listen Hydrate(Guid id, Guid postId,Listener listener, string username, string ipAddress, Instant created)
        {
            return new Listen(id,postId,listener,username,ipAddress,created);
        }
    }
}
