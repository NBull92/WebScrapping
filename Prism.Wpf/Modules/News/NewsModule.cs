using Infrastructure;
using News.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace News
{
    public class NewsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ClubNewsRegion, typeof(ClubNews));
            regionManager.RegisterViewWithRegion(RegionNames.LeagueNewsRegion, typeof(LeagueNews));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}