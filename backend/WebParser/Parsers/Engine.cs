using AngleSharp;
using AngleSharp.Html.Parser;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebParser.Model;
using WebParser.Dom;

namespace WebParser.Parsers
{
    public class Engine
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private IConfiguration config = Configuration.Default;
        private static OfferDom selectedOffer = SelectedOfferDom.Instance;
        private static OfferDom unSelectedOffer = UnSelectedOfferDom.Instance;

        public async Task<List<Offer>> ParseAsync(string html)
        {
            try
            {
                var context = BrowsingContext.New(config);
                var parser = context.GetService<IHtmlParser>();
                var document = await parser.ParseDocumentAsync(html);
                //var items = document.QuerySelectorAll("div.cdk-virtual-scroll-content-wrapper");
                var items = document.QuerySelectorAll("div.row-layout.row-wrap.card-unread");
                var selectedItem = document.QuerySelectorAll("div.row-layout.row-wrap.card-selected.card-current-cursor");
                var result = new List<Offer>(items.Length + selectedItem.Length);
                result.AddRange(items.Select(c => unSelectedOffer.ParseItem(c)));
                result.AddRange(selectedItem.Select(c => selectedOffer.ParseItem(c)));
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e, "Parse failed");
                throw;
            }                        
        }
    }
}
