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
            var location = PostcodeApi.GetLatLonFromPostcode();
            var stopPoints = TflApi.GetStopPointsFromLocation(location);

            if (stopPoints.Count == 0)
            {
                Logger.Error($"No bus stops were found for the given location of \"{location}\"");
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
                Logger.Error($"No buses were found for the given location of \"{location}\"");
                Console.WriteLine("No buses found for this location");
                return;
            }

            if (UserInput.DoesUserWantDirections())
            {
                Trip.PrintDirectionsToBusStop(stopPoints);
                return;
            }
        }
        
        public static void CheckActiveDisruptions()
        {
            throw new NotImplementedException();
        }
    }
}