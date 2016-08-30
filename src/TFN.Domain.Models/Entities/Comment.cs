using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Comment : DomainEntity<Guid>
    {
        public Comment(Guid id)
            : base(id)
        {
            
        }

    }
}
