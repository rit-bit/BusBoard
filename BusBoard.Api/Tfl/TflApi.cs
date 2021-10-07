using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Postcode;
using NLog;
using RestSharp;

namespace BusBoard.Tfl
{
    public static class TflApi
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static readonly RestClient Client = new RestClient("https://api.tfl.gov.uk/");

        public static IEnumerable<StopPointArrival> Get5BusesForStopPoint(string stopPoint)
        {
            var request = new RestRequest($"StopPoint/{stopPoint}/Arrivals", Method.GET);

            var response = Client.Get<List<StopPointArrival>>(request);
            return response.Data.Take(5);
        }

        public static List<StopPoint> GetStopPointsFromLocation(LocationInfo location)
        {
            const string stopTypes = "NaptanPublicBusCoachTram";
            var lat = location.latitude;
            var lon = location.longitude;
            var request = new RestRequest($"/StopPoint?stopTypes={stopTypes}&lat={lat}&lon={lon}&radius=500", Method.GET);
            var response = Client.Get<StopPointList>(request);
            var data = response.Data;
            return data.stopPoints;
        }

        public static Trip GetDirections(string from, string to)
        {
            var request = new RestRequest($"Journey/JourneyResults/{from}/to/{to}");
            var response = Client.Get<Trip>(request);
            var data = response.Data;
            return data;
        }

        public static ActiveDisruptionsList CheckDisruptionsForAll(string modes)
        {
            var request = new RestRequest($"/Line/Mode/{modes}/Disruption");
            var response = Client.Get<ActiveDisruptionsList>(request);
            var data = response.Data;
            return data;
        }
    }
}