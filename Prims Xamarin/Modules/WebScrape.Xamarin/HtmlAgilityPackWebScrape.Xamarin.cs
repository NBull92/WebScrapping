//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrape.Xamarin
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.Xamarin;

    public class HtmlAgilityPackWebScrape : IWebScrapeService
    {
        private readonly HtmlWeb _web;

        public HtmlAgilityPackWebScrape()
        {
            _web = new HtmlWeb();
        }

        public HtmlDocument LoadPage(string webPage)
        {
            return _web.Load(webPage);
        }

        public List<HtmlNode> SelectNode(HtmlDocument document, string path)
        {
            try
            {
                return document.DocumentNode.SelectNodes(path).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<HtmlNode> SelectViaClass(HtmlDocument document, string className, string element = "div")
        {
            try
            {
                return document.DocumentNode.Descendants(element).Where(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Contains(className)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
// WebScrape.Xamarin namespace 