using AngleSharp.Html.Parser;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace WebParser.Utils
{
    public class Converters
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private const string NA_INT = "—";
        private static CultureInfo provider = new CultureInfo("en-US");

        public static decimal? GetPriceOf(AngleSharp.Dom.IElement item, string selector)
        {
            var str = GetTextOf(item, selector) ?? throw new ArgumentNullException($"Failed to parse NULL for {selector}");
            if (string.Compare(NA_INT, str) == 0)
                return null;
            if (decimal.TryParse(str, NumberStyles.Currency, provider, out decimal value))
                return value;
            throw new HtmlParseException(0, $"Failed to parse price from {str}", AngleSharp.Text.TextPosition.Empty);
        }

        [return: MaybeNull]
        public static int? GetIntOf(AngleSharp.Dom.IElement item, string selector)
        {
            var str = GetTextOf(item, selector) ?? throw new ArgumentNullException($"Failed to parse NULL for {selector}");
            if (string.Compare(NA_INT, str) == 0)
                return null;
            if (int.TryParse(str, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out int value))
                return value;
            throw new HtmlParseException(0, $"Failed to parse int from {str}", AngleSharp.Text.TextPosition.Empty);
        }

        [return: MaybeNull]
        public static string GetTextOf([NotNull] AngleSharp.Dom.IElement item, string selector)
        {
            try
            {
                return item?.QuerySelector(selector)?.TextContent?.Trim();
            }
            catch (Exception e)
            {
                logger.Error(e, "Failed to parse row: {0} for {1}", item.OuterHtml, selector);
                throw;
            }
        }
    }
}
