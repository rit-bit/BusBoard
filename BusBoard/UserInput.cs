using System;
using BusBoard.Postcode;

namespace BusBoard
{
    public class UserInput
    {
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

                switch (response.ToLower())
                {
                    case "y":
                        return true;

                    case "n":
                        return false;
                    
                    default:
                        Console.WriteLine("You entered an invalid option. Please type Y or N");
                        break;
                }
            }
        }

        public static int WhichBusStop(string busStop1, string busStop2)
        {
            Console.WriteLine($"Which bus stop would you like directions to? Type 1 for {busStop1} or 2 for {busStop2}");

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
                        Console.WriteLine("You entered an invalid option. Please type 1 or 2");
                        break;
                }
            }
        }
    }
}