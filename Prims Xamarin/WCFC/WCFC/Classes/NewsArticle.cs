//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WCFC.Classes
{
    using HtmlAgilityPack;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using WCFC.Views;
    using System.Diagnostics;
    using System.Linq;
    using Infrastructure.Xamarin;

    public class NewsArticle : BindableBase
    {
        private readonly INavigationService _navigationService;

        private ArticleType _articleType;
        private string _header;
        private string _link;
        private string _imageSource;
        private string _date;
        private string _subHeader;

        public ArticleType ArticleType
        {
            get => _articleType;
            set => SetProperty(ref _articleType, value);
        }

        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
        }

        public string Link
        {
            get => _link;
            set => SetProperty(ref _link, value);
        }

        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public string SubHeader
        {
            get => _subHeader;
            set => SetProperty(ref _subHeader, value);
        }

        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public DelegateCommand OpenLink { get; set; }

        public NewsArticle(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OpenLink = new DelegateCommand(Open);
        }

        public void CreateArticle(HtmlNode node, ArticleType articleType)
        {
            ArticleType = articleType;

            if (node.Attributes["data-href"] != null)
            {
                Link = node.Attributes["data-href"].Value;
            }

            if (node.ChildNodes.Any(o => o.Attributes["class"] != null && o.Attributes["class"].Value.Contains("media__img")))
            {
                var media__img = node.ChildNodes.FirstOrDefault(o => o.Attributes["class"] != null && o.Attributes["class"].Value.Contains("media__img"));
                if (media__img.ChildNodes.Any(o => o.Name == "a"))
                {
                    var outerHtml = media__img.ChildNodes.FirstOrDefault(o => o.Name == "a").OuterHtml;
                    ImageSource = StringHelper.GetImageSourceFromHtml(outerHtml);
                }
            }

            if (node.ChildNodes.Any(o => o.Attributes["class"] != null && o.Attributes["class"].Value.Contains("media__body")))
            {
                var media__body = node.ChildNodes.FirstOrDefault(o => o.Attributes["class"] != null && o.Attributes["class"].Value.Contains("media__body"));

                if (media__body.ChildNodes.Any(o => o.Name == "a"))
                {
                    Header = StringHelper.FindAndReplaceHtmlCodes(media__body.ChildNodes.FirstOrDefault(o => o.Name == "a").InnerHtml);
                }

                if (media__body.ChildNodes.Any(o => o.Name == "p"))
                {
                    SubHeader = StringHelper.FindAndReplaceHtmlCodes(media__body.ChildNodes.FirstOrDefault(o => o.Name == "p").InnerHtml);
                }

                if (media__body.ChildNodes.Any(o => o.Name == "span"))
                {
                    var date = StringHelper.FindAndReplaceHtmlCodes(media__body.ChildNodes.FirstOrDefault(o => o.Name == "span").InnerText.Replace("\n", "").TrimEnd().TrimStart());
                    Date = date;
                }
            }

            Debug.WriteLine($"header: {Header} subheader: {SubHeader} " +
                            $"imagesource: {ImageSource} date: {Date} link: {Link}");
        }

        private void Open()
        {
            var parameters = new NavigationParameters();
            parameters.Add("NewsArticle", this);

            // Go to 'news article' page and load the news article via it's link
            _navigationService.NavigateAsync(nameof(NewsArticlePage), parameters);
        }
    }
}
// WCFC.Classes namespace 