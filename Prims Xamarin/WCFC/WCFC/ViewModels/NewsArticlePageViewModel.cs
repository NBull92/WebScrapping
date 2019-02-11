//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------


using System;

namespace WCFC.ViewModels
{
    using Infrastructure.Xamarin;
    using Prism.Commands;
    using Prism.Navigation;
    using System.Linq;
    using System.Threading;
    using WCFC.Classes;
    using Xamarin.Forms;

    public class NewsArticlePageViewModel : ViewModelBase
    {
        private readonly IWebScrapeService _webScrape;

        private NewsArticle _newsArticle;
        private bool _isBusy;
        private string _imageSource;
        private string _message;
        private string _header;
        private string _secondaryHeader;

        public NewsArticle NewsArticle
        {
            get => _newsArticle;
            set => SetProperty(ref _newsArticle, value);
        }
        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        public string SecondaryHeader
        {
            get => _secondaryHeader;
            set => SetProperty(ref _secondaryHeader, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public DelegateCommand OpenLink { get; set; }

        public NewsArticlePageViewModel(INavigationService navigationService, IWebScrapeService webScrape)
            : base(navigationService)
        {
            _webScrape = webScrape;
            OpenLink = new DelegateCommand(Open);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("NewsArticle"))
            {
                IsBusy = true;
                var thread = new Thread(() =>
                {
                    var article = parameters.FirstOrDefault(a => a.Key == "NewsArticle").Value as NewsArticle;
                    NewsArticle = article;
                    Title = $"{article?.ArticleType} News";

                    var page = _webScrape.LoadPage(article.Link);
                    
                    var selectedNodes = _webScrape.SelectViaClass(page, "u-space-bottom--large u-clearfix", "div");

                    foreach (var node in selectedNodes)
                    {
                        if (node.ChildNodes.Any(o => o.Name == "p"))
                        {
                            foreach (var child in node.ChildNodes.Where(o => o.Name == "p"))
                            {
                                if(string.IsNullOrEmpty(child.InnerText))
                                    continue;

                                Message += string.Join("\n", child.InnerText);
                            }
                        }
                    }
                    
                    IsBusy = false;
                });
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        private void Open()
        {
            Device.OpenUri(new Uri(NewsArticle.Link));
        }
    }
}
// WCFC.ViewModels namespace 