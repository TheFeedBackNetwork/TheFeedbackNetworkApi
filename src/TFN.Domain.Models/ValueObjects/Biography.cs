namespace TFN.Domain.Models.ValueObjects
{
    public class Biography
    {
        public string Text { get; private set; }
        public string InstagramUrl { get; private set; }
        public string SoundCloudUrl { get; private set; }
        public string WebsiteUrl { get; private set; }

        public Biography(string text, string instagramUrl, string soundCloudUrl, string websiteUrl)
        {
            //Domain allows for null & empties here
            Text = text;
            InstagramUrl = instagramUrl;
            SoundCloudUrl = soundCloudUrl;
            WebsiteUrl = websiteUrl;
        }

        public static Biography From(string text, string instagramUrl, string soundCloudUrl, string websiteUrl)
        {
            return new Biography(text, instagramUrl, soundCloudUrl, websiteUrl);
        }
    }
}
