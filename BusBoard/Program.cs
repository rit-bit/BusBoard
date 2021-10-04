using System;
using System.Collections.Generic;
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

            var response = client.Get<List<DataObject>>(request);
            var data = response.Data;
            Console.WriteLine(data);

        }

        static string GetUserInput()
        {
            Console.WriteLine("Input stop code:");
            return Console.ReadLine();
        }
    }
}