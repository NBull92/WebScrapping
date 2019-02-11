//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WCFC.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using HtmlAgilityPack;
    using Infrastructure.Xamarin;
    using Prism.Navigation;
    using WCFC.Classes;
    using Xamarin.Forms;

    public class LeagueNewsViewModel : ViewModelBase
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

        public LeagueNewsViewModel(INavigationService navigationService, IWebScrapeService webScrape)
            : base(navigationService)
        {
            _webScrape = webScrape;
            NewsList = new ObservableCollection<NewsArticle>();
            Title = "League News";
            RefreshNews = new DelegateCommand(Refresh);
        }

        private void Refresh()
        {
            IsBusy = true;
            NewsList = new ObservableCollection<NewsArticle>();

            var thread = new Thread(() =>
            {
                var page = _webScrape.LoadPage("http://www.worcestercityfc.org/league-news/7862");

                var selectedNodes = _webScrape.SelectViaClass(page, "c-news-tile", "div");

                foreach (var node in selectedNodes)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var article = new NewsArticle(NavigationService);
                        article.CreateArticle(node, ArticleType.League);
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
// WCFC.ViewModels namespace 
