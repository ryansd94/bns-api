using HtmlAgilityPack;
using System.Collections.Generic;

namespace BNS.Utilities
{
    public static class HtmlHelper
    {
        public static List<string> GetDataAttributeFromHtmlString(string htmlString, string attribute)
        {
            var result = new List<string>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var divNodes = htmlDocument.DocumentNode.SelectNodes("//*[@" + attribute + "]");
            foreach (var divNode in divNodes)
            {
                var value = divNode.GetAttributeValue(attribute, "");
                result.Add(value);
            }

            return result;
        }
    }
}
