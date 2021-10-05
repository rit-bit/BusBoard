using System;
using BusBoard.Postcode;
using BusBoard.Tfl;

namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var result = UserInput.GetMainAction();

                switch (result)
                {
                    case UserInput.MainAction.JourneyPlanner:
                        PlanJourney();
                        break;

                    case UserInput.MainAction.NearestBusStop:
                        GetNearestBusStopsAndDirections();
                        break;
                }
            }
        }

        public static void PlanJourney()
        {
            var destination = UserInput.GetPostcodeInput("Input destination postcode: ");
            Trip.PrintDirections(Trip.Softwire, destination);
        }

        public static void GetNearestBusStopsAndDirections()
        {
            var location = PostcodeApi.GetLatLonFromPostcode();
            var stopPoints = TflApi.GetStopPointsFromLocation(location);

            if (stopPoints.Count == 0)
            {
                Console.WriteLine("There are no bus stops for the given postcode. Try a different postcode");
                return;
            }

            var busList = StopPointArrival.GetArrivalList(stopPoints);

            foreach (var busArrival in busList)
            {
                Console.WriteLine(busArrival);
            }

            if (busList.Count == 0)
            {
                Console.WriteLine("No buses found for this location");
                return;
            }

            if (UserInput.DoesUserWantDirections())
            {
                Trip.PrintDirectionsToBusStop(stopPoints);
                return;
            }
        }
    }
}