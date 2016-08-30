using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class User : DomainEntity<Guid>
    {
        public string Username { get; set; }
        public IReadOnlyCollection<Claim> Claims { get; set; }
        public User(Guid id)
            : base(id)
        {
            
        }
    }
}
