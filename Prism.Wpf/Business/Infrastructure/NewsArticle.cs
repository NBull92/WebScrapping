//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace Infrastructure
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Diagnostics;

    public class NewsArticle : BindableBase
    {
        private string _header;
        private string _link;

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

        public DelegateCommand OpenLink { get; set; }

        public NewsArticle(string header, string link)
        {
            Header = header;
            Link = StripHref(link);

            OpenLink = new DelegateCommand(Open);
        }

        private void Open()
        {
            Process.Start(Link);
        }

        private string StripHref(string aHref)
        {
            var output = aHref.Split('"')[1];
            return output;
        }
    }
}
// Infrastructure namespace 