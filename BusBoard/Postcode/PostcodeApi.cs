using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.Tfl;
using RestSharp;

namespace BusBoard.Postcode
{
    public static class PostcodeApi
    {
        static RestClient client = new RestClient("https://api.postcodes.io/");
        public static LocationInfo GetLatLonFromPostcode()
        {
            while (true)
            {
                var postcode = UserInput.GetPostcodeInput("Input postcode:");
                var request = new RestRequest($"postcodes?q={postcode}", Method.GET);

                var response = client.Get<List<PostcodeInfo>>(request);
                var data = response.Data;

                if (data[0].result.Count > 0)
                {
                    return data[0].result[0]; 
                }

                else
                {
                    Console.WriteLine();
                }

            }

        }
        public static bool IsPostcodeValid(string postcode)
        {
            var request = new RestRequest($"postcodes/{postcode}", Method.GET);

            var response = client.Get<Status>(request);
            var data = response.Data;

            return data.status == 200;

        }
        
    }
}