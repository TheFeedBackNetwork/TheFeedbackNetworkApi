namespace TFN.Domain.Models.ValueObjects
{
    public class Biography
    {
        public string Text { get; private set; }
        public string TwitterUrl { get; private set; }
        public string YouTubeUrl { get; private set; }
        public string FacebookUrl { get; private set; }
        public string InstagramUrl { get; private set; }
        public string SoundCloudUrl { get; private set; }
        public string Location { get; private set; }

        public Biography(string text, string instagramUrl, string soundCloudUrl
                    , string twitterUrl, string youTubeUrl,string facebookUrl,string location)
        {
            Text = text;
            InstagramUrl = instagramUrl;
            SoundCloudUrl = soundCloudUrl;
            TwitterUrl = twitterUrl;
            YouTubeUrl = youTubeUrl;
            FacebookUrl = facebookUrl;
            Location = location;
        }

        public static Biography From(string text, string instagramUrl, string soundCloudUrl
            , string twitterUrl, string youTubeUrl, string facebookUrl, string location)
        {
            return new Biography(text, instagramUrl, soundCloudUrl,twitterUrl,youTubeUrl,facebookUrl,location);
        }
    }
}
