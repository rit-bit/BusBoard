using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Postcode;
using NLog;
using RestSharp;

namespace BusBoard.Tfl
{
    public class TflApi
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static RestClient client = new RestClient("https://api.tfl.gov.uk/");
        
        public static IEnumerable<StopPointArrival> Get5BusesForStopPoint(string stopPoint)
        {
            var request = new RestRequest($"StopPoint/{stopPoint}/Arrivals", Method.GET);

            var response = client.Get<List<StopPointArrival>>(request);
            return response.Data.Take(5);
        }

        public static List<StopPoint> GetStopPointsFromLocation(LocationInfo location)
        {
            const string stopTypes = "NaptanPublicBusCoachTram";
            var lat = location.latitude;
            var lon = location.longitude;
            var request = new RestRequest($"/StopPoint?stopTypes={stopTypes}&lat={lat}&lon={lon}&radius=500", Method.GET);
            var response = client.Get<StopPointList>(request);
            var data = response.Data;
            return data.stopPoints;
        }

        public static Trip GetDirections(string from, string to)
        {
            var request = new RestRequest($"Journey/JourneyResults/{from}/to/{to}");
            var response = client.Get<Trip>(request);
            var data = response.Data;
            return data;
        }
    }
}