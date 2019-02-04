//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WCFC.Classes
{
    using HtmlAgilityPack;
    using Infrastructure.Xamarin;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using WCFC.Views;

    public class NewsArticle : BindableBase
    {
        private readonly INavigationService _navigationService;

        private string _header;
        private string _link;
        private ArticleType _articleType;

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

        public ArticleType ArticleType
        {
            get => _articleType;
            set => SetProperty(ref _articleType, value);
        }

        public DelegateCommand OpenLink { get; set; }

        public NewsArticle(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OpenLink = new DelegateCommand(Open);
        }

        public void CreateArticle(HtmlNode node, ArticleType articleType)
        {
            Header = StringHelper.FindAndReplaceHtmlCodes(node.InnerText);
            Link = StringHelper.StripHref(node.OuterHtml);
            ArticleType = articleType;
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