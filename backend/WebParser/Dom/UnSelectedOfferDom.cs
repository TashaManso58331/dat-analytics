namespace WebParser.Dom
{
    class UnSelectedOfferDom : OfferDom
    {
        public static OfferDom Instance = new UnSelectedOfferDom();
        private UnSelectedOfferDom()
        {
            OriginName = "div.flex-overflow-hidden.origin > span.locale.origin-locale.truncate > span.truncate";
            OriginState = "div.flex-overflow-hidden.origin > span.locale.origin-locale.truncate > span:nth-child(3)";
            OriginDeadHead = "div.flex-overflow-hidden.origin > span.origin-destination-deadhead > span:nth-child(2)";
            DestinationName = "div.flex-overflow-hidden.destination > span > span.locale.destination-locale.truncate > span > span.truncate";
            DestinationState = "div.flex-overflow-hidden.destination > span > span.locale.destination-locale.truncate > span > span:nth-child(3)";
            DestinationDeadHead = "div.flex-overflow-hidden.destination > span > span.origin-destination-deadhead > span:nth-child(2)";
            Trip = "div.truck-section > div.lower-cell.trip-cell > div > div > a";
            LengthValue = "div.row-layout.row-wrap > div.truck-section > div.lower-cell.length-cell > div > div > span.amount";
            LengthUnit = "div.row-layout.row-wrap > div.truck-section > div.lower-cell.length-cell > div > div > span.unit";
            WeightValue = "div.row-layout.row-wrap > div.truck-section > div.lower-cell.weight-cell > div > div > span.amount";
            WeightUnit = "div.row-layout.row-wrap > div.truck-section > div.lower-cell.weight-cell > div > div > span.unit";
            Price = "div.row-layout.row-wrap > div.lower-cell.rate-cell.ng-star-inserted > div";
            CompanyName = "div.row-layout.row-wrap.card-unread > div.contact-section.truncate > a > span.contact-link";
            CompanyContact = "div.row-layout.row-wrap.card-unread > div.contact-section.truncate > div > a";
            Age = "div.row-layout.row-wrap.card-unread > div.age-section > div.lower-cell.age-cell > div > div";
        }
    }
}
