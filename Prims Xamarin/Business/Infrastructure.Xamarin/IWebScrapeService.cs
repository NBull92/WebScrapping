
namespace Infrastructure.Xamarin
{
    using HtmlAgilityPack;
    using System.Collections.Generic;

    public interface IWebScrapeService
    {
        HtmlDocument LoadPage(string webPage);
        List<HtmlNode> SelectNode(HtmlDocument document, string path);
    }
}
// Infrastructure.Xamarin namespace 