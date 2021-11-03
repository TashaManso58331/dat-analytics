using System.Collections.Generic;

namespace Dat.Access.Loads.Request
{
    public class DesiredSpecs
    {
        public List<string> equipmentTypes { get; set; }
        public List<string> equipmentClasses { get; set; }
        public string capacity { get; set; }
        public int? maxLengthInFeet { get; set; }
        public int? maxWeightInPounds { get; set; }
    }

    public class Deadhead
    {
        public int miles { get; set; }
        public string type { get; set; }
    }

    public class Point
    {
        public string city { get; set; }
        public string state { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Deadhead deadhead { get; set; }
    }

    public class Area
    {
        public List<string> states { get; set; }
        public List<string> zones { get; set; }
    }

    public class Origin
    {
        public Point point { get; set; }
        public Area area { get; set; }
    }

    public class Destination
    {
        public Point point { get; set; }
        public Area area { get; set; }
    }

    public class Availability
    {
        public string earliest { get; set; }
        public string latest { get; set; }
    }

    public class Criteria
    {
        public DesiredSpecs desiredSpecs { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public int maxAgeInMinutes { get; set; }
        public Availability availability { get; set; }
    }

    public class SearchRequest
    {
        public Criteria criteria { get; set; }
    }
}
