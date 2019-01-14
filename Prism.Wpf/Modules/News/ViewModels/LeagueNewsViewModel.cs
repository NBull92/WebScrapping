//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace News.ViewModels
{
    using HtmlAgilityPack;
    using Infrastructure;
    using Infrastructure.Interface;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows;

    public class LeagueNewsViewModel : BindableBase
    {
        private readonly IWebScrapeService _webScrape;

        private string _message;
        private ObservableCollection<NewsArticle> _newsList;
        private bool _isBusy;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ObservableCollection<NewsArticle> NewsList
        {
            get => _newsList;
            set => SetProperty(ref _newsList, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public DelegateCommand RefreshNews { get; set; }

        public LeagueNewsViewModel(IWebScrapeService webScrape)
        {
            _webScrape = webScrape;
            NewsList = new ObservableCollection<NewsArticle>();
            Message = "League News";
            RefreshNews = new DelegateCommand(Refresh);
        }

        private void Refresh()
        {
            IsBusy = true;

            var thread = new Thread(() =>
            {
                var page = _webScrape.LoadPage("http://www.worcestercityfc.org/league-news/7862");

                var nodeClasses = new string[1] { "//a[@class='heading__title u-delta']" };

                var nodes = new ObservableCollection<HtmlNode>();

                foreach (var nodeClass in nodeClasses)
                {
                    var selectedNodes = _webScrape.SelectNode(page, nodeClass);

                    if (selectedNodes != null)
                        nodes.AddRange(selectedNodes);
                }

                foreach (var node in nodes)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        NewsList.Add(new NewsArticle(node.InnerText, node.OuterHtml));
                    });
                }

                IsBusy = false;
            });
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
// News.ViewModelsnamespace 