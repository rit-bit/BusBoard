using System;
using System.Collections.Generic;
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
            var fileTarget = new FileTarget { FileName = @"C:\Work\Logs\BusBoard.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            var consoleTarget = new ConsoleTarget { Name = "Console", Layout = @"${message}" };
            config.AddTarget("File Logger", fileTarget);
            config.AddTarget("Console", consoleTarget);
            config.AddRule(LogLevel.Error, LogLevel.Fatal, "Console");
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            LogManager.Configuration = config;
        }

        public static void PlanJourney()
        {
            var destination = UserInput.GetPostcodeInput("Input destination postcode: ");
            Trip.PrintDirections(Trip.Softwire, destination);
            //TODO exception thrown when journey planned from softwire to softwire
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
                return;
            }

            if (UserInput.DoesUserWantDirections())
            {
                var location = PostcodeApi.GetLatLonFromPostcode(postcode);
                var stopPoints = TflApi.GetStopPointsFromLocation(location);
                var index = GetNextBusStopIndex(stopPoints);
                Trip.PrintDirectionsToBusStop(stopPoints[index]);
            }
        }
        private static int GetNextBusStopIndex(List<StopPoint> stopPoints)
        {
            if (stopPoints.Count > 1)
            {
                return UserInput.WhichBusStop(stopPoints[0].commonName, stopPoints[1].commonName);
            }
            
            return 0;
        }
        
        public static void CheckActiveDisruptions()
        {
            throw new NotImplementedException();
        }
    }
}