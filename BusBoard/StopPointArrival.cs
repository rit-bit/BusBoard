using System;

namespace BusBoard
{
    public class StopPointArrival
    {
        public string LineName { get; set; }
        public int TimeToStation { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string Towards { get; set; }
        
        public override String ToString()
        {
            var time = ConvertSecondsToMinutesSeconds(TimeToStation);
            return $"{LineName} towards {Towards} expected at {ExpectedArrival:t} (in {time})";
        }

        private string ConvertSecondsToMinutesSeconds(int seconds)
        {
            var mins = seconds / 60;
            var secs = seconds % 60;
            return $"{mins}m {secs}s";
        }
    }
}