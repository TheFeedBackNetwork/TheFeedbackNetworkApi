using System;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ITrackRepository : IAddableRepository<Track, Guid>, IDeleteableRepository<Track,Guid>
    {

    }
}
