#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var journey in journeys)
            {
                builder.AppendLine($"OPTION {option++}:");
                builder.AppendLine(journey.ToString());
            }
            return builder.ToString();
        }

        public static void PrintDirectionsToBusStop(StopPoint stopPoint)
        {
            PrintDirections(Softwire, stopPoint.naptanID);
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
        
        public bool isDisrupted { get; set; }
        
        public override string ToString()
        {
            // If any leg is disrupted, whole journey is flagged as disrupted
            isDisrupted = legs.Any(leg => leg.isDisrupted);

            var builder = new StringBuilder();
            
            if (isDisrupted)
            {
                builder.AppendLine("This journey is disrupted");
                
            }
            builder.AppendLine(string.Join('\n', legs));
            return builder.ToString();
        }
    }

    public class Leg
    {
        public Instruction instruction { get; set; }
        public int duration { get; set; }
        
        public bool isDisrupted { get; set; }
        
        public List<Disruption> disruptions { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            
            if (isDisrupted)
            {
                builder.AppendLine("There are disruptions for this part of the journey");
                builder.AppendLine(string.Join('\n', disruptions.Distinct().ToList()));
            }
            builder.AppendLine($"{instruction} for {ConvertMinutesToHoursMinutes(duration)}");
            
            return builder.ToString();
        }
        
        private static string ConvertMinutesToHoursMinutes(int minutes)
        {
            var hours = minutes / 60;
            minutes = minutes % 60;
            return $"{hours}h {minutes}m";
        }
    }

    public class Disruption
    {
        public string description { get; set; }
        public string summary { get; set; }
        public string additionalInfo { get; set; }

        public override string ToString()
        {
            return description;
        }

        // TODO - Double check if this needs to be a custom method
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() == GetType())
            {
                var other = (Disruption) obj;
                if (description != other.description)
                {
                    return false;
                }

                if (summary != other.summary)
                {
                    return false;
                }

                return additionalInfo == other.additionalInfo;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hash = 7 * description.GetHashCode();
            hash *= 11 * summary.GetHashCode();
            hash *= 13 * additionalInfo.GetHashCode();
            return hash;
        }
    }

    public class Instruction
    {
        public List<Step> steps { get; set; } = null!;
        public string summary { get; set; }
        public string detailed { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder(summary);
            if (steps.Count == 0)
            {
                builder.Append('(');
                builder.Append(detailed);
                builder.AppendLine(")");
                return builder.ToString();
            }
            builder.AppendLine(steps[0].FirstToString());
            for (var i = 1; i < steps.Count; i++)
            {
                builder.AppendLine(steps[i].ToString());
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