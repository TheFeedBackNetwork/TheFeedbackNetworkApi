using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class BiographyResponseModel
    {
        public string Text { get; private set; }
        public string TwitterUrl { get; private set; }
        public string YouTubeUrl { get; private set; }
        public string FacebookUrl { get; private set; }
        public string InstagramUrl { get; private set; }
        public string SoundCloudUrl { get; private set; }
        public string Location { get; private set; }

        private BiographyResponseModel(string text, string instagramUrl, string soundCloudUrl
            , string twitterUrl, string youTubeUrl, string facebookUrl, string location)
        {
            Text = text;
            InstagramUrl = instagramUrl;
            SoundCloudUrl = soundCloudUrl;
            TwitterUrl = twitterUrl;
            YouTubeUrl = youTubeUrl;
            FacebookUrl = facebookUrl;
            Location = location;
        }

        public static BiographyResponseModel From(Biography biography)
        {
            return new BiographyResponseModel(
                biography.Text,
                biography.InstagramUrl,
                biography.SoundCloudUrl,
                biography.TwitterUrl,
                biography.YouTubeUrl,
                biography.FacebookUrl,
                biography.Location
                );
        }
    }
}
