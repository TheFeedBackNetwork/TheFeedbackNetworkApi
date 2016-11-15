using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TrackAggregate.InMemory
{
    public class InMemoryTracks
    {
        public static List<Track> Tracks = new List<Track>();
    }
}
