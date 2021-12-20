using Dat.Model;
using System;
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

        public override bool Equals(object obj)
        {
            return obj is DesiredSpecs specs &&
                   EqualityComparer<List<string>>.Default.Equals(equipmentTypes, specs.equipmentTypes) &&
                   EqualityComparer<List<string>>.Default.Equals(equipmentClasses, specs.equipmentClasses) &&
                   capacity == specs.capacity &&
                   maxLengthInFeet == specs.maxLengthInFeet &&
                   maxWeightInPounds == specs.maxWeightInPounds;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(equipmentTypes, equipmentClasses, capacity, maxLengthInFeet, maxWeightInPounds);
        }
    }

    public class Deadhead
    {
        public int miles { get; set; }
        public string type { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Deadhead deadhead &&
                   miles == deadhead.miles &&
                   type == deadhead.type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(miles, type);
        }
    }

    public class Point
    {
        public string city { get; set; }
        public string state { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Deadhead deadhead { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   city == point.city &&
                   state == point.state &&
                   latitude == point.latitude &&
                   longitude == point.longitude &&
                   EqualityComparer<Deadhead>.Default.Equals(deadhead, point.deadhead);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(city, state, latitude, longitude, deadhead);
        }
    }

    public class Area
    {
        public List<string> states { get; set; }
        public List<string> zones { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Area area &&
                   EqualityComparer<List<string>>.Default.Equals(states, area.states) &&
                   EqualityComparer<List<string>>.Default.Equals(zones, area.zones);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(states, zones);
        }
    }

    public class Origin
    {
        public Point point { get; set; }
        public Area area { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Origin origin &&
                   EqualityComparer<Point>.Default.Equals(point, origin.point) &&
                   EqualityComparer<Area>.Default.Equals(area, origin.area);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(point, area);
        }
    }

    public class Destination
    {
        public Point point { get; set; }
        public Area area { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Destination destination &&
                   EqualityComparer<Point>.Default.Equals(point, destination.point) &&
                   EqualityComparer<Area>.Default.Equals(area, destination.area);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(point, area);
        }
    }

    public class Availability
    {
        public string earliest { get; set; }
        public string latest { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Availability availability &&
                   earliest == availability.earliest &&
                   latest == availability.latest;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(earliest, latest);
        }
    }

    public class Criteria
    {
        public DesiredSpecs desiredSpecs { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public int maxAgeInMinutes { get; set; }
        public Availability availability { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Criteria criteria &&
                   EqualityComparer<DesiredSpecs>.Default.Equals(desiredSpecs, criteria.desiredSpecs) &&
                   EqualityComparer<Origin>.Default.Equals(origin, criteria.origin) &&
                   EqualityComparer<Destination>.Default.Equals(destination, criteria.destination) &&
                   maxAgeInMinutes == criteria.maxAgeInMinutes &&
                   EqualityComparer<Availability>.Default.Equals(availability, criteria.availability);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(desiredSpecs, origin, destination, maxAgeInMinutes, availability);
        }
    }

    public class SearchRequest
    {
        public Criteria criteria { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SearchRequest request &&
                   EqualityComparer<Criteria>.Default.Equals(criteria, request.criteria);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(criteria);
        }
    }
}
