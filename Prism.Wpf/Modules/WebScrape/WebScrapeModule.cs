//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace WebScrape
{
    using Infrastructure.Interface;
    using Prism.Ioc;
    using Prism.Modularity;

    public class WebScrapeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IWebScrapeService, HtmlAgilityPackWebScrape>();
        }
    }
}
// WebScrape namespace 