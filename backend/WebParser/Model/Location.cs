using Newtonsoft.Json;
using System;

namespace WebParser.Model
{
    public class Location
    {
        public int? DeadHead { get; set; }
        public string Name { get; set; }
        public string State { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Location location &&
                   DeadHead == location.DeadHead &&
                   Name == location.Name &&
                   State == location.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DeadHead, Name, State);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
