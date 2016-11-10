using System;
using System.Collections.Generic;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.Domain.Models.Entities
{
    public class Track : DomainEntity<Guid>, IAggregateRoot
    {
        public Uri Location { get; private set; }
        public Guid UserId { get; private set; }
        public IReadOnlyList<int> SoundWave { get; private set; }

        private Track(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave)
            : base(id)
        {
            if (soundWave.Count != 4000)
            {
                throw new ArgumentException($"The soundwave [{nameof(soundWave)}] needs to be 4000 elements long");
            }

            UserId = userId;
            Location = location;
            SoundWave = soundWave;
        }

        public Track(Guid userId, Uri location, IReadOnlyList<int> soundWave)
            : this(Guid.NewGuid(), userId, location, soundWave)
        {
            
        }

        public static Track Hydrate(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave)
        {
            return new Track(id,userId,location,soundWave);
        }

    }
}
