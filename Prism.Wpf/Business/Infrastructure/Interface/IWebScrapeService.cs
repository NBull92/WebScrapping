//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace Infrastructure.Interface
{
    using HtmlAgilityPack;
    using System.Collections.Generic;

    public interface IWebScrapeService
    {
        HtmlDocument LoadPage(string webPage);
        List<HtmlNode> SelectNode(HtmlDocument document, string path);
    }
}
//Infrastructure.Interface namespace 