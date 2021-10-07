using System;
using System.Collections.Generic;

namespace BusBoard.Tfl
{
    public class StopPointList
    {
        public List<StopPoint> stopPoints { get; set; }
    }

    public class StopPoint
    {
        public string naptanID { get; set; }
        public string commonName { get; set; }
        public float distance { get; set; }

        public override string ToString()
        {
            return $"{commonName} ({naptanID})";
        }
    }
}