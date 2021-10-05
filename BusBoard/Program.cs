using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopCode = GetUserInput();
            var client = new RestClient("https://api.tfl.gov.uk/");
            var request = new RestRequest($"StopPoint/{stopCode}/Arrivals", Method.GET);

            var response = client.Get<List<StopPointArrival>>(request);
            var data = response.Data;
            
            foreach (var stopPointArrival in data.OrderBy(item => item.TimeToStation).Take(5))
            {
                Console.WriteLine(stopPointArrival);
            }


        }

        static string GetUserInput()
        {
            Console.WriteLine("Input stop code:");
            return Console.ReadLine();
        }
    }
}