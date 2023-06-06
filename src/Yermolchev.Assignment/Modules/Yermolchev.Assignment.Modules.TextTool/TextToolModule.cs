using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Yermolchev.Assignment.Core;
using Yermolchev.Assignment.Modules.TextTool.Views;

namespace Yermolchev.Assignment.Modules.TextTool
{
    public class TextToolModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public TextToolModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(SelectFile));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SelectFile>();
            containerRegistry.RegisterForNavigation<ProcessFile>();
            containerRegistry.RegisterForNavigation<ShowResults>();
        }
    }
}