using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Tfl
{
    public class StopPointArrival
    {
        public string LineName { get; set; }
        public int TimeToStation { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string Towards { get; set; }
        public string StopName { get; set; }
        
        public override string ToString()
        {
            var time = ConvertSecondsToMinutesSeconds(TimeToStation);
            return $"{LineName} from {StopName} towards {Towards} expected at {ExpectedArrival:t} (in {time})";
        }

        private static string ConvertSecondsToMinutesSeconds(int seconds)
        {
            var minutes = seconds / 60;
            seconds = seconds % 60;
            return $"{minutes}m {seconds}s";
        }

        public static List<StopPointArrival> GetArrivalList(List<StopPoint> stopPoints)
        {
            var busList = new List<StopPointArrival>();
            for (var i = 0; i < 2; i++)
            {
                var stopPoint = stopPoints[i];
                var list = TflApi.Get5BusesForStopPoint(stopPoint.naptanID);
                foreach (var item in list)
                {
                    item.StopName = stopPoint.commonName;
                    busList.Add(item);
                }
            }

            // TODO - return busList.OrderBy(bus => bus.ExpectedArrival);

            busList.Sort(new Comparison<StopPointArrival>(StopPointArrival.Comparison));
            return busList;

        }

        public static int Comparison(StopPointArrival x, StopPointArrival y)
        {
            return x.TimeToStation - y.TimeToStation;
        }
    }
}