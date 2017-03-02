using System;
using System.Collections.Generic;
using TFN.Domain.Models.ValueObjects;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Track : DomainEntity<Guid>, IAggregateRoot
    {
        public Uri Location { get; private set; }
        public Guid UserId { get; private set; }
        public TrackMetaData TrackMetaData { get; private set; }
        public IReadOnlyList<int> SoundWave { get; private set; }
        public DateTime Created { get; private set; }

        public Track(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
            : base(id)
        {
            UserId = userId;
            Location = location;
            SoundWave = soundWave;
            TrackMetaData = metaData;
            Created = created;
        }

        public Track(Guid userId, Uri location, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
            : this(Guid.NewGuid(), userId, location, soundWave,metaData, created)
        {
            
        }

        public static Track Hydrate(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave,TrackMetaData metaData, DateTime created)
        {
            return new Track(id,userId,location,soundWave,metaData,created);
        }

    }
}
