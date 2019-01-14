//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrape
{
    using System;
    using System.Collections.Generic;
    using HtmlAgilityPack;
    using Infrastructure.Interface;
    using System.Linq;

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
    }
}
// WebScrape namespace 