using System;
using System.Collections.Generic;
using System.Text;

namespace BusBoard.Tfl
{
    public class Trip
    {
        public const string Softwire = "NW5 1TL";
        public List<Journey> journeys { get; set; }

        public override string ToString()
        {
            if (journeys.Count == 0)
            {
                return "";
            }
            var builder = new StringBuilder();
            var option = 'A';
            builder.Append($"\nOPTION {option}:");
            builder.Append('\n');
            builder.Append(journeys[0]);
            for (var i = 1; i < journeys.Count; i++)
            {
                option++;
                var journey = journeys[i];
                builder.Append($"\n\nOPTION {option}:");
                builder.Append('\n');
                builder.Append(journey);
            }

            return builder.ToString();
        }

        public static void PrintDirectionsToBusStop(List<StopPoint> stopPoints)
        {
            var index = 0;
            if (stopPoints.Count >= 2)
            {
                index = UserInput.WhichBusStop(stopPoints[0].commonName, stopPoints[1].commonName);
            }
            PrintDirections(Softwire, stopPoints[index].naptanID);
        }

        public static void PrintDirections(string from, string to)
        {
            var directions = TflApi.GetDirections(from, to);
            Console.WriteLine(directions);
        }
    }

    public class Journey
    {
        public int duration { get; set; }
        public List<Leg> legs { get; set; }
        
        public override string ToString()
        {
            return string.Join('\n', legs);
        }
    }

    public class Leg
    {
        public Instruction instruction { get; set; }
        public int duration { get; set; }

        public override string ToString()
        {
            var output = $"{instruction} for {ConvertMinutesToHoursMinutes(duration)}";
            if (instruction.steps.Count == 0)
            {
                output += ')';
            }
            return output;
        }
        
        private static string ConvertMinutesToHoursMinutes(int minutes)
        {
            var hours = minutes / 60;
            minutes = minutes % 60;
            return $"{hours}h {minutes}m";
        }
    }

    public class Instruction
    {
        public List<Step> steps { get; set; }
        public string summary { get; set; }
        public string detailed { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder(summary);
            if (steps.Count == 0)
            {
                builder.Append("\n(");
                builder.Append(detailed);
                return builder.ToString();
            }
            builder.Append('\n');
            builder.Append(steps[0].FirstToString());
            for (var i = 1; i < steps.Count; i++)
            {
                builder.Append('\n');
                builder.Append(steps[i]);
            }

            return builder.ToString();
        }
    }

    public class Step
    {
        public string descriptionHeading { get; set; }
        public string description{ get; set; }
        public string skyDirectionDescription{ get; set; }

        public override string ToString()
        {
            return $"{descriptionHeading.Trim()} {description.Trim()}";
        }
        public string FirstToString()
        {
            return $"{descriptionHeading.Trim()} {description.Trim()} ({skyDirectionDescription})";
        }
    }
}