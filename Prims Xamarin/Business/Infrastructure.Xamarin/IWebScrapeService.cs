
namespace Infrastructure.Xamarin
{
    using HtmlAgilityPack;
    using System.Collections.Generic;

    public interface IWebScrapeService
    {
        HtmlDocument LoadPage(string webPage);
        List<HtmlNode> SelectNode(HtmlDocument document, string path);
        List<HtmlNode> SelectViaClass(HtmlDocument document, string path, string element = "div");
    }
}
// Infrastructure.Xamarin namespace 