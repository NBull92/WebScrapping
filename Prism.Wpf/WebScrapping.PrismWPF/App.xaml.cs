//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrapping.PrismWPF
{
    using Infrastructure.Interface;
    using Prism.Ioc;
    using Prism.Modularity;
    using System.Windows;
    using Views;
    using WebScrape;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterSingleton<IWebScrapeService, HtmlAgilityPackWebScrape>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<News.NewsModule>();
        }
    }
}
// WebScrapping.PrismWPF namespace 