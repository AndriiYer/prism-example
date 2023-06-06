using System.Collections.Generic;
using Prism.Regions;
using Yermolchev.Assignment.Core.Mvvm;

namespace Yermolchev.Assignment.Modules.TextTool.ViewModels
{
    public class ShowResultsViewModel : RegionViewModelBase
    {
        private Dictionary<string, int> _wordsStatistics;
        public Dictionary<string, int> WordsStatistics
        {
            get { return _wordsStatistics; }
            set { SetProperty(ref _wordsStatistics, value); }
        }

        public ShowResultsViewModel(IRegionManager regionManager)
            : base(regionManager) { }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            WordsStatistics = navigationContext.Parameters[nameof(ProcessFileViewModel.WordsStatistics)] as Dictionary<string, int>;
        }
    }
}
