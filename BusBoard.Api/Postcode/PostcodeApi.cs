using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Tfl;
using NLog;
using RestSharp;

namespace BusBoard.Postcode
{
    public static class PostcodeApi
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static RestClient client = new RestClient("https://api.postcodes.io/");
        public static LocationInfo GetLatLonFromPostcode(string postcode)
        {
            while (true)
            {
                var request = new RestRequest($"postcodes?q={postcode}", Method.GET);

                var response = client.Get<List<PostcodeInfo>>(request);
                var data = response.Data;

                if (data[0].result.Count > 0)
                {
                    return data[0].result[0]; 
                }

                else
                {
                    Logger.Debug($"Postcode API did not send valid response to request for postcode \"{postcode}\"");
                    Console.WriteLine(); // To separate old input from new input
                }

            }

        }
        public static bool IsPostcodeValid(string postcode)
        {
            var request = new RestRequest($"postcodes/{postcode}", Method.GET);

            var response = client.Get<Status>(request);
            var data = response.Data;
            var toReturn = data.status == 200;
            
            var validity = toReturn ? "pass" : "fail";
            Logger.Debug($"Postcode \"{postcode}\" checked for validity, result: {validity}");
            
            return toReturn;
        }
        
    }
}