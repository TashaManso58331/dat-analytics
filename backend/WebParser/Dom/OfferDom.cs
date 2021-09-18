using AngleSharp.Dom;
using System;
using System.Diagnostics.CodeAnalysis;
using WebParser.Model;
using WebParser.Utils;

namespace WebParser.Dom
{
    abstract class OfferDom
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string OriginName { get; protected set; }
        public string OriginState { get; protected set; }
        public string OriginDeadHead { get; protected set; }
        public string DestinationName { get; protected set; }
        public string DestinationState { get; protected set; }
        public string DestinationDeadHead { get; protected set; }
        public string Trip { get; protected set; }
        public string LengthValue { get; protected set; }
        public string LengthUnit { get; protected set; }
        public string WeightValue { get; protected set; }
        public string WeightUnit { get; protected set; }
        public string Price { get; protected set; }
        public string CompanyName { get; protected set; }
        public string CompanyContact { get; protected set; }
        public string Age { get; protected set; }

        [return: NotNull]
        internal Offer ParseItem(IElement el)
        {

            try
            {
                return new Offer()
                {
                    Origin = new Location()
                    {
                        Name = Converters.GetTextOf(el, this.OriginName),
                        State = Converters.GetTextOf(el, this.OriginState),
                        DeadHead = Converters.GetIntOf(el, this.OriginDeadHead),                        
                    },
                    Destination = new Location()
                    {
                        Name = Converters.GetTextOf(el, this.DestinationName),
                        State = Converters.GetTextOf(el, this.DestinationState),
                        DeadHead = Converters.GetIntOf(el, this.DestinationDeadHead),
                    },
                    Trip = Converters.GetIntOf(el, this.Trip),
                    Length = new Amount()
                    {
                        Value = Converters.GetIntOf(el, this.LengthValue),
                        Unit = Converters.GetTextOf(el, this.LengthUnit),
                    },
                    Weight = new Amount()
                    {
                        Value = Converters.GetIntOf(el, this.WeightValue),
                        Unit = Converters.GetTextOf(el, this.WeightUnit),
                    },
                    Price = Converters.GetPriceOf(el, this.Price),
                    Company = new Company()
                    {
                        Name = Converters.GetTextOf(el, this.CompanyName),
                        Contact = Converters.GetTextOf(el, this.CompanyContact),
                    },
                    Age = Converters.GetTextOf(el, this.Age),
                };
            }
            catch (Exception e)
            {
                logger.Error(e, "Failed to parse row: {0}", el.OuterHtml);
                throw;
            }
        }
    }
}
