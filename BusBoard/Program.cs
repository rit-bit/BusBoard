using System;
using BusBoard.Postcode;
using BusBoard.Tfl;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace BusBoard
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            InitLogging();
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
                    
                    case UserInput.MainAction.ActiveDisruptions:
                        CheckActiveDisruptions();
                        break;
                }
            }
        }
        

        private static void InitLogging()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            // TODO - Add console target and remove duplicated lines in other files
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
        }

        public static void PlanJourney()
        {
            var destination = UserInput.GetPostcodeInput("Input destination postcode: ");
            Trip.PrintDirections(Trip.Softwire, destination);
            //TODO execption throw when journey planned from softwire to softwire
        }

        public static void GetNearestBusStopsAndDirections()
        {
            var postcode = UserInput.GetPostcodeInput("Input postcode:");
            var busList = TflApi.GetBusesForTwoNearestStops(postcode);

            foreach (var busArrival in busList)
            {
                Console.WriteLine(busArrival);
            }

            if (busList.Count == 0)
            {
                Logger.Error($"No buses were found for the given location of \"{postcode}\"");
                Console.WriteLine("No buses found for this location");
                return;
            }

            if (UserInput.DoesUserWantDirections())
            {
                var location = PostcodeApi.GetLatLonFromPostcode(postcode);
                var stopPoints = TflApi.GetStopPointsFromLocation(location);
                // TODO - var index = GetNextBusStopIndex();
                var index = 0;
                if (stopPoints.Count > 1)
                {
                   index = UserInput.WhichBusStop(stopPoints[0].commonName, stopPoints[1].commonName);
                }
                Trip.PrintDirectionsToBusStop(stopPoints[index]);
            }
        }
        
        public static void CheckActiveDisruptions()
        {
            throw new NotImplementedException();
        }
    }
}