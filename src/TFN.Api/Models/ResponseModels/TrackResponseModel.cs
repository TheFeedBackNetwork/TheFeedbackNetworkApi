using System;
using System.Collections.Generic;
using TFN.Api.Extensions;
using TFN.Api.Filters.ActionFilters;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class TrackResponseModel : ResourceResponseModel
    {
        public Guid UserId { get; private set; }
        public Uri Location { get; private set; }
        [Excludable]
        public IReadOnlyList<int> SoundWave100 { get; private set; }
        [Excludable]
        public IReadOnlyList<int> SoundWave300 { get; private set; }
        [Excludable]
        public IReadOnlyList<int> SoundWave4000 { get; private set; }
        public DateTime Created { get; private set; }

        private TrackResponseModel(Guid id, Guid userId, Uri location, IReadOnlyList<int> soundWave, DateTime created, string apiUrl)
            : base(GetHref(id,apiUrl), id)
        {
            UserId = userId;
            Location = location;
            SoundWave4000 = soundWave;
            SoundWave300 = soundWave.DownSampleTo(300);
            SoundWave100 = soundWave.DownSampleTo(100);
            Created = created;
        }

        private static Uri GetHref(Guid id, string apiUrl)
        {
            return new Uri($"{apiUrl}/api/tracks/{id}");
        }

        internal static TrackResponseModel From(Track track, string apiUrl )
        {
            return new TrackResponseModel(
                track.Id,
                track.UserId,
                track.Location,
                track.SoundWave,
                track.Created,
                apiUrl
                );
        }
    }
}
