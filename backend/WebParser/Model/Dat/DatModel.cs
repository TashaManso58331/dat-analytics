using System;
using System.Collections.Generic;

namespace WebParser.Model.Dat
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Origin
    {
        public int id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string county { get; set; }
        public string type { get; set; }
    }

    public class Destination
    {
        public int id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string county { get; set; }
        public string type { get; set; }
    }

    public class Availability
    {
        public DateTime earliest { get; set; }
        public DateTime latest { get; set; }
    }

    public class ContactName
    {
        public string first { get; set; }
        public string last { get; set; }
        public string initials { get; set; }
    }

    public class Callback
    {
        public string phone { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public string phoneExt { get; set; }
    }

    public class Credit
    {
        public int score { get; set; }
        public int daysToPay { get; set; }
    }

    public class MatchDetail
    {
        public int ageInMilliseconds { get; set; }
        public string matchId { get; set; }
        public DateTime pickupDate { get; set; }
        public string equipmentType { get; set; }
        public string equipmentTypeCode { get; set; }
        public DateTime servicedDate { get; set; }
        public DateTime endDate { get; set; }
        public string companyName { get; set; }
        public bool isLoad { get; set; }
        public string assetType { get; set; }
        public Origin origin { get; set; }
        public Destination destination { get; set; }
        public Availability availability { get; set; }
        public bool isPartial { get; set; }
        public bool isActive { get; set; }
        public string officeId { get; set; }
        public string combinedOfficeId { get; set; }
        public string registryLookupId { get; set; }
        public int weight { get; set; }
        public int length { get; set; }
        public string referenceState { get; set; }
        public int tripMiles { get; set; }
        public bool isTripMilesAir { get; set; }
        public int originDeadheadMiles { get; set; }
        public bool isOriginDeadheadMilesAir { get; set; }
        public List<string> comments { get; set; }
        public ContactName contactName { get; set; }
        public Callback callback { get; set; }
        public Credit credit { get; set; }
        public bool isTiaMember { get; set; }
        public bool isP3Member { get; set; }
        public bool isFactorable { get; set; }
        public bool isAssurable { get; set; }
        public double rate { get; set; }
        public string rateBasedOn { get; set; }
        public string addressCity { get; set; }
        public string addressState { get; set; }
        public bool isTriumphPay { get; set; }
        public int searcherMcNumber { get; set; }
        public int searcherDotNumber { get; set; }
        public string postersReferenceId { get; set; }
    }

    public class MatchCounts
    {
        public int exactMatchCount { get; set; }
        public int similarMatchCount { get; set; }
    }

    public class Root
    {
        public string searchId { get; set; }
        public int totalMatchCount { get; set; }
        public List<MatchDetail> matchDetails { get; set; }
        public List<object> similarMatchDetails { get; set; }
        public MatchCounts matchCounts { get; set; }
    }

}
