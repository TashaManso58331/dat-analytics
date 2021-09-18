using System.Collections.Generic;

namespace Dat.Model.States
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Count
    {
        public string state { get; set; }
        public int inbound { get; set; }
        public int outbound { get; set; }
    }

    public class InboundOutbound
    {
        public List<Count> counts { get; set; }
    }

    public class Root
    {
        public InboundOutbound inboundOutbound { get; set; }
    }
}
