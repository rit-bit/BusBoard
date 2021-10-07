using System.Collections.Generic;

namespace BusBoard.Tfl
{
    public class ActiveDisruptionsList
    {
        public List<ActiveDisruption> activeDisruptions { get; set; }
    }

    public class ActiveDisruption
    {
        public string description { get; set; }
    }
}

