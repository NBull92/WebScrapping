﻿//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WCFC.ViewModels
{
    using HtmlAgilityPack;
    using Infrastructure.Xamarin;
    using Prism.Commands;
    using Prism.Navigation;
    using System.Collections.ObjectModel;
    using System.Threading;
    using WCFC.Classes;
    using Xamarin.Forms;

    public class ClubNewsViewModel : ViewModelBase
    {
        private readonly IWebScrapeService _webScrape;

        private ObservableCollection<NewsArticle> _newsList;
        private bool _isBusy;

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

        public ClubNewsViewModel(INavigationService navigationService, IWebScrapeService webScrape)
            : base(navigationService)
        {
            _webScrape = webScrape;
            NewsList = new ObservableCollection<NewsArticle>();
            Title = "Club News";
            RefreshNews = new DelegateCommand(Refresh);
        }

        private void Refresh()
        {
            IsBusy = true;
            NewsList = new ObservableCollection<NewsArticle>();

            var thread = new Thread(() =>
            {
                var page = _webScrape.LoadPage("http://www.worcestercityfc.org/news/");

                var nodeClasses = new string[2] { "//a[@class='heading__title u-gamma']", "//a[@class='heading__title u-delta']" };

                var nodes = new ObservableCollection<HtmlNode>();

                foreach (var nodeClass in nodeClasses)
                {
                    var selectedNodes = _webScrape.SelectNode(page, nodeClass);

                    if (selectedNodes == null)
                        continue;

                    foreach (var node in selectedNodes)
                    {
                        nodes.Add(node);
                    }
                }

                foreach (var node in nodes)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var article = new NewsArticle(NavigationService);
                        article.CreateArticle(node, ArticleType.Club);
                        NewsList.Add(article);
                    });
                }

                IsBusy = false;
            });
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            Refresh();
        }
    }
}
// namespace WCFC.ViewModels