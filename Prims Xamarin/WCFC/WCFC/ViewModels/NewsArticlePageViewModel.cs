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
    using System.Text.RegularExpressions;
    using System.Threading;
    using HtmlAgilityPack;
    using Infrastructure.Xamarin;
    using Prism.Navigation;
    using WCFC.Classes;
    using Xamarin.Forms;

    public class NewsArticlePageViewModel : ViewModelBase
    {
        private readonly IWebScrapeService _webScrape;

        private bool _isBusy;
        private string _imageSource;
        private string _message;
        private string _header;
        private string _secondaryHeader;

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


        public NewsArticlePageViewModel(INavigationService navigationService, IWebScrapeService webScrape)
            : base(navigationService)
        {
            _webScrape = webScrape;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("NewsArticle"))
            {
                var article = parameters.FirstOrDefault(a => a.Key == "NewsArticle").Value as NewsArticle;
                Title = $"{article?.ArticleType} News";
                Header = article?.Header;

                IsBusy = true;

                var thread = new Thread(() =>
                {
                    var page = _webScrape.LoadPage(article.Link);
                    
                    var imgNode = _webScrape.SelectNode(page, "//img[@class='peephole__image ']");
                    if (imgNode != null)
                    {
                        ImageSource = StringHelper.GetImageSourceFromHtml(imgNode.FirstOrDefault()?.OuterHtml);
                    }


                    var headerNode = _webScrape.SelectNode(page, "//h3[@class='u-space-bottom']");
                    if (headerNode != null)
                    {
                        SecondaryHeader = StringHelper.FindAndReplaceHtmlCodes(headerNode.FirstOrDefault()?.InnerHtml);
                    }

                    IsBusy = false;
                });
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }
    }
}
// WCFC.ViewModels namespace 