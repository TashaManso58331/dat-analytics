using Newtonsoft.Json;
using System;

namespace WebParser.Model
{
    public class Amount
    {
        public int? Value { get; set; }
        public string Unit { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Amount amount &&
                   Value == amount.Value &&
                   Unit == amount.Unit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Unit);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
