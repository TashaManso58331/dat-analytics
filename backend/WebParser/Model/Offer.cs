using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebParser.Model
{
    public class Offer
    {
        public static Offer Empty { get; internal set; } = new Offer();

        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public int? Trip { get; set; }
        public decimal? Price { get; set; }
        public string Age { get; set; }
        public Amount Length { get; set; }
        public Amount Weight { get; set; }
        public Company Company { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Offer offer &&
                   EqualityComparer<Location>.Default.Equals(Origin, offer.Origin) &&
                   EqualityComparer<Location>.Default.Equals(Destination, offer.Destination) &&
                   Trip == offer.Trip &&
                   Price == offer.Price &&
                   Age == offer.Age &&
                   EqualityComparer<Amount>.Default.Equals(Length, offer.Length) &&
                   EqualityComparer<Amount>.Default.Equals(Weight, offer.Weight) &&
                   EqualityComparer<Company>.Default.Equals(Company, offer.Company);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Destination, Trip, Price, Age, Length, Weight, Company);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}
