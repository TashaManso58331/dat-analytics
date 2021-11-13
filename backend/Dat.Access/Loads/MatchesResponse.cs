using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dat.Access.Loads.Matches
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Availability
    {
        public DateTime earliest { get; set; }
        public DateTime latest { get; set; }
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

    public class Destination
    {
        public Point point { get; set; }
    }

    public class Specs
    {
        public string requiredEquipmentType { get; set; }
        public string requiredCapacity { get; set; }
        public int lengthInFeet { get; set; }
        public int weightInPounds { get; set; }
    }

    public class Comment
    {
        public string comment { get; set; }
    }

    public class BaseRate
    {
        public int amount { get; set; }
        public string currency { get; set; }
    }

    public class Rate
    {
        public BaseRate baseRate { get; set; }
        public string rateBasedOn { get; set; }
    }

    public class Credit
    {
        public int score { get; set; }
        public int daysToPay { get; set; }
        public DateTime scoreUpdatedWhen { get; set; }
    }

    public class TripLength
    {
        public string type { get; set; }
        public int distanceMiles { get; set; }
    }

    public class Phone
    {
        public string number { get; set; }
    }

    public class Contact
    {
        public Phone phone { get; set; }
    }

    public class Poster
    {
        public string company { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public Contact contact { get; set; }
        public string referenceId { get; set; }
        public string registryLookupId { get; set; }
        public bool hasTiaMembership { get; set; }
    }

    public class MatchesResponse
    {
        public string postingId { get; set; }
        public string version { get; set; }
        public string matchId { get; set; }
        public string status { get; set; }
        public int relevance { get; set; }
        public DateTime createdWhen { get; set; }
        public DateTime modifiedWhen { get; set; }
        public Availability availability { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public string factoring { get; set; }
        public Specs specs { get; set; }
        public List<Comment> comments { get; set; }
        public bool isAssurable { get; set; }
        public Rate rate { get; set; }
        public Credit credit { get; set; }
        public TripLength tripLength { get; set; }
        public bool isFromPrivateNetwork { get; set; }
        public Poster poster { get; set; }
    }
}
