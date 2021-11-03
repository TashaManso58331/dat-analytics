using System;
using System.Collections.Generic;

namespace Dat.Access.Loads.Response
{
    public class Owner
    {
        public int userId { get; set; }
        public int officeId { get; set; }
        public int companyId { get; set; }
    }

    public class DesiredSpecs
    {
        public List<string> equipmentClasses { get; set; }
        public string capacity { get; set; }
        public int maxWeightInPounds { get; set; }
        public List<object> equipmentTypes { get; set; }
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

    public class Origin
    {
        public Point point { get; set; }
    }

    public class Area
    {
        public List<string> zones { get; set; }
    }

    public class Destination
    {
        public Area area { get; set; }
    }

    public class Availability
    {
        public DateTime earliest { get; set; }
        public DateTime latest { get; set; }
    }

    public class Criteria
    {
        public DesiredSpecs desiredSpecs { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public int maxAgeInMinutes { get; set; }
        public Availability availability { get; set; }
    }

    public class SearchResponse
    {
        public string id { get; set; }
        public string version { get; set; }
        public Owner owner { get; set; }
        public string assetType { get; set; }
        public Criteria criteria { get; set; }
        public DateTime createdWhen { get; set; }
        public DateTime modifiedWhen { get; set; }
        public DateTime expiresWhen { get; set; }
    }

}
