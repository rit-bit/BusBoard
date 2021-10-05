using System;
using System.Collections.Generic;
using System.Text;

namespace BusBoard.Tfl
{
    public class Trip
    {
        public List<Journey> journeys { get; set; }

        public override string ToString()
        {
            return string.Join('\n', journeys);
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

        public override string ToString()
        {
            return instruction.ToString();
        }
    }

    public class Instruction
    {
        public List<Step> steps { get; set; }

        public override string ToString()
        {
            if (steps.Count == 0)
            {
                return "";
            }
            var builder = new StringBuilder();
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