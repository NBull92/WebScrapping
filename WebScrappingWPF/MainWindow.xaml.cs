//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrappingWPF
{
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly  List<News> _news = new List<News>();

        public MainWindow()
        {
            InitializeComponent();

            Scrape();
        }

        private void Scrape()
        {
            var web = new HtmlWeb();
            var doc = web.Load("http://www.worcestercityfc.org/news/");

            var headerNames2 = doc.DocumentNode.SelectNodes("//a[@class='heading__title u-gamma']").ToList();
            var headerNames3 = doc.DocumentNode.SelectNodes("//a[@class='heading__title u-delta']").ToList();

            foreach (var headerName in headerNames2)
            {
                var news = CreateNews(headerName);
                _news.Add(news);
            }

            foreach (var headerName in headerNames3)
            {
                var news = CreateNews(headerName);
                _news.Add(news);
            }

            NewsList.ItemsSource = _news;
        }

        private News CreateNews(HtmlNode headerName)
        {
            var news = new News
            {
                Header = headerName.InnerText,
                Link = StripHref(headerName.OuterHtml)
            };
            return news;
        }

        private string StripHref(string aHref)
        {
            string input = "<a href=\"http://www.worcestercityfc.org/news/next-home-game-2380287.html\" class=\"heading__title u-gamma\">NEXT HOME GAME</a>";

            var output = input.Split('"')[1];
            return output;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var test = ((Button) sender).DataContext as News;
            Process.Start(test.Link);
        }
    }

    public class News
    {
        public string Header { get; set; }
        public string Link { get; set; }
    }
}
// WebScrappingWPF namespace 
