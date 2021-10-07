using System.Collections.Generic;
using BusBoard.Tfl;

namespace BusBoard.Web.Models
{
    public class HomeViewModel
    {
        public List<StopPointArrival> buses { get; set; }
        public int blah = 5;
    }
}