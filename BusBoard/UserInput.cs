using System;
using BusBoard.Postcode;
using NLog;

namespace BusBoard
{
    public class UserInput
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static string GetPostcodeInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine($"\n {prompt}");

                var postCode = Console.ReadLine();

                if (PostcodeApi.IsPostcodeValid(postCode))
                {
                    return postCode;
                }

                else
                {
                    Logger.Error($"User entered an invalid postcode of \"{postCode}\"");
                    Console.WriteLine("Invalid Postcode. Please enter a new one");
                }
            }
        }

        public static bool DoesUserWantDirections()
        {
            Console.WriteLine("\nWould you like directions to one of the bus stops? Type Y or N");
            while (true)
            {
                var response = Console.ReadLine();
                if (response != null)
                {
                    switch (response.ToLower())
                    {
                        case "y":
                            return true;

                        case "n":
                            return false;
                    }
                }

                Logger.Error($"User entered an invalid option of \"{response}\" instead of \"Y\" or \"N\".");
                Console.WriteLine("You entered an invalid option. Please type Y or N");
            }
        }

        public static int WhichBusStop(string busStop1, string busStop2)
        {
            Console.WriteLine(
                $"Which bus stop would you like directions to? Type 1 for {busStop1} or 2 for {busStop2}");

            while (true)
            {
                var response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        return 0;

                    case "2":
                        return 1;

                    default:
                        Logger.Error(
                            $"User entered an invalid option of \"{response}\" for which bus stop to get directions to.");
                        Console.WriteLine("You entered an invalid option. Please type 1 or 2");
                        break;
                }
            }
        }

        public static MainAction GetMainAction()
        {
            Console.WriteLine(
                $"\nWhich action would you like to do? Type 1 for Journey Planner and 2 for Nearest Bus Stop");

            while (true)
            {
                var response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        return MainAction.JourneyPlanner;

                    case "2":
                        return MainAction.NearestBusStop;

                    default:
                        Logger.Error($"User entered an invalid Main Menu action option of \"{response}\"");
                        Console.WriteLine("You entered an invalid option. Please type 1 or 2");
                        break;
                }
            }
        }

        public enum MainAction
        {
            JourneyPlanner,
            NearestBusStop
        }
    }
}