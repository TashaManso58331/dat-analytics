using Newtonsoft.Json;
using System;

namespace WebParser.Model
{
    public class Company
    {
        public string Name { get; set; }
        public string Contact { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Company company &&
                   Name == company.Name &&
                   Contact == company.Contact;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Contact);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}