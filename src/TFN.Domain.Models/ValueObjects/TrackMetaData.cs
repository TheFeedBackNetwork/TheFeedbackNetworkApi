namespace TFN.Domain.Models.ValueObjects
{
    public class TrackMetaData
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }
        public double TotalHours { get; private set; }
        public double TotalMinutes { get; private set; }
        public double TotalMilliseconds { get; private set; }
        public long Ticks { get; private set; }

        private TrackMetaData(int hours, int minutes, int seconds, double totalHours, double totalMinutes,
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

        public static TrackMetaData From(int hours, int minutes, int seconds, double totalHours, double totalMinutes,
            double totalMilliseconds, long ticks)
        {
            return new TrackMetaData(hours,minutes,seconds,totalHours,totalMinutes,totalMilliseconds,ticks);
        }


    }
}
