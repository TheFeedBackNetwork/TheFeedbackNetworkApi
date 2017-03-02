using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class TrackMetaDataResponseModel
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public double TotalHours { get; private set; }
        public double TotalMinutes { get; private set; }
        public double TotalMilliseconds { get; private set; }
        public long Ticks { get; private set; }

        private TrackMetaDataResponseModel(int hours, int minutes, int seconds, double totalHours, double totalMinutes,
            double totalMilliseconds, long ticks)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            TotalHours = totalHours;
            TotalMinutes = totalMinutes;
            TotalMilliseconds = totalMilliseconds;
            Ticks = ticks;
        }

        public static TrackMetaDataResponseModel From(TrackMetaData trackMetadata)
        {
            return new TrackMetaDataResponseModel(trackMetadata.Hours, trackMetadata.Minutes, trackMetadata.Seconds,trackMetadata.TotalHours,trackMetadata.TotalMinutes,trackMetadata.TotalMilliseconds,trackMetadata.Ticks);
        }
    }
}
