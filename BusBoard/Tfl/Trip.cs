using System.Collections.Generic;
using System.Text;

namespace BusBoard.Tfl
{
    public class Trip
    {
        public List<Journey> journeys { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var journey in journeys)
            {
                builder.Append(journey);
                builder.Append('\n');
            }
            return builder.ToString();
        }
    }

    public class Journey
    {
        public int duration { get; set; }
        public List<Leg> legs { get; set; }
        
        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var leg in legs)
            {
                builder.Append(leg);
                builder.Append('\n');
            }
            return builder.ToString();
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
            var builder = new StringBuilder();
            foreach (var step in steps)
            {
                builder.Append(step);
                builder.Append('\n');
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
            return $"{descriptionHeading} {description}";
        }
    }
}