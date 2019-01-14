//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrapping
{
    using HtmlAgilityPack;
    using System;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            var web = new HtmlWeb();
            var doc = web.Load(
                "https://www.yellowpages.com/search?search_terms=software&geo_location_terms=Sydney%2C+ND");

            var headerNames = doc.DocumentNode.SelectNodes("//a[@class='business-name']").ToList();

            foreach (var headerName in headerNames)
            {
                Console.WriteLine(headerName.InnerText);
            }

            var web2 = new HtmlWeb();
            var doc2 = web2.Load("http://www.worcestercityfc.org/news/");

            var headerNames2 = doc2.DocumentNode.SelectNodes("//a[@class='heading__title u-gamma']").ToList();
            var headerNames3 = doc2.DocumentNode.SelectNodes("//a[@class='heading__title u-delta']").ToList();

            foreach (var headerName in headerNames2)
            {
                var news = CreateNews(headerName);

                Console.WriteLine(news.Header + " " + news.Link);
            }

            foreach (var headerName in headerNames3)
            {
                var news = CreateNews(headerName);

                Console.WriteLine(news.Header + " " + news.Link);
            }
        }

        private static News CreateNews(HtmlNode headerName)
        {
            var news = new News
            {
                Header = headerName.InnerText,
                Link = StripHref(headerName.OuterHtml)
            };
            return news;
        }

        static string StripHref(string aHref)
        {
           string input = "<a href=\"http://www.worcestercityfc.org/news/next-home-game-2380287.html\" class=\"heading__title u-gamma\">NEXT HOME GAME</a>";

           var output = input.Split('"')[1];
           return output;
        }
    }
}
// WebScrapping namespace 