using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BusBoard.Postcode;
using BusBoard.Tfl;
using RestSharp;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var location = PostcodeApi.GetLatLonFromPostcode();
                Console.WriteLine(location);
                var stopPoints = TflApi.GetStopPointsFromLocation(location);

                if (stopPoints.Count == 0)
                {
                    Console.WriteLine("There are no bus stops for the given postcode. Try a different postcode");
                    continue;
                }

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

                busList.Sort(new Comparison<StopPointArrival>(StopPointArrival.Comparison));
                foreach (var busArrival in busList)
                {
                    Console.WriteLine(busArrival);
                }

                if (busList.Count == 0)
                {
                    Console.WriteLine("No buses found for this location");
                    continue;
                }

                if (UserInput.DoesUserWantDirections())
                {
                    var index = 0;
                    if (stopPoints.Count >= 2)
                    {
                        index = UserInput.WhichBusStop(stopPoints[0].commonName, stopPoints[1].commonName);
                    }

                    var directions = TflApi.GetDirections("NW5 1TL", stopPoints[index].naptanID);
                    Console.WriteLine(directions);
                    return;
                }
            }
        }
    }
}