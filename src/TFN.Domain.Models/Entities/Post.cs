using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Post : DomainEntity<Guid>
    {
        public string TrackUrl { get; private set; }
        public string Text { get; private set; }
        public Instant Created { get; private set; }
        public Instant Modified { get; private set; }
        public int Likes { get; private set; }
        public IReadOnlyCollection<string> Tags { get; private set; }
        public Post(Guid id)
            : base(id)
        {
            
        }
    }
}
