using System.Collections.Generic;
using System.Threading;
using Prism.Commands;
using Prism.Regions;
using Yermolchev.Assignment.Core;
using Yermolchev.Assignment.Core.Mvvm;
using Yermolchev.Assignment.Modules.TextTool.Views;
using Yermolchev.Assignment.Services.Interfaces;

namespace Yermolchev.Assignment.Modules.TextTool.ViewModels
{
    public class ProcessFileViewModel : RegionViewModelBase
    {
        private ITextParsingService _textParsingService;

        private CancellationTokenSource _cancellationTokenSource = new ();

        private DelegateCommand _seeResultsCommand;
        public DelegateCommand SeeResultsCommand =>
            _seeResultsCommand ??= new DelegateCommand(SeeResults, CanSeeResults);

        private DelegateCommand _cancelTextProcessingCommand;
        public DelegateCommand CancelTextProcessingCommand =>
            _cancelTextProcessingCommand ??= new DelegateCommand(CancelTextProcessing, CanCancelTextProcessing);

        private Dictionary<string, int> _wordsStatistics;
        public Dictionary<string, int> WordsStatistics
        {
            get { return _wordsStatistics; }
            set { SetProperty(ref _wordsStatistics, value); }
        }

        public ProcessFileViewModel(IRegionManager regionManager, ITextParsingService textParsingService)
            : base (regionManager) 
        {
            _textParsingService = textParsingService;
            _textParsingService.ParsingProgressChanged += _textParsingService_ParsingProgressChanged;
        }

        private bool CanCancelTextProcessing() => !(_cancellationTokenSource.IsCancellationRequested || TotallyToProcess == CurrentlyProcessed);

        private void CancelTextProcessing()
        {
            _cancellationTokenSource.Cancel();
            SeeResultsCommand.RaiseCanExecuteChanged();
            CancelTextProcessingCommand.RaiseCanExecuteChanged();
            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(SelectFile));
        }

        private void _textParsingService_ParsingProgressChanged(object sender, ProgressInfoEventArgs e)
        {
            TotallyToProcess = e.TotallyToProcess;
            CurrentlyProcessed = e.CurrentlyProcessed;
            SeeResultsCommand.RaiseCanExecuteChanged();
            CancelTextProcessingCommand.RaiseCanExecuteChanged();

        }

        private double _totallyToProcess;
        public double TotallyToProcess
        {
            get { return _totallyToProcess; }
            set { SetProperty(ref _totallyToProcess, value); }
        }

        private double currentlyProcessed;
        public double CurrentlyProcessed
        {
            get { return currentlyProcessed; }
            set { SetProperty(ref currentlyProcessed, value); }
        }

        private bool CanSeeResults() => !_cancellationTokenSource.IsCancellationRequested && TotallyToProcess == CurrentlyProcessed;

        private void SeeResults()
        {
            var parameters = new NavigationParameters
            {
                { nameof(WordsStatistics), WordsStatistics }
            };

            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ShowResults), parameters);
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var path = navigationContext.Parameters[nameof(SelectFileViewModel.FileName)] as string;
            WordsStatistics = await _textParsingService.GetWordsStatistics(path, _cancellationTokenSource.Token);
        }

        public override void Destroy()
        {
            _textParsingService.ParsingProgressChanged -= _textParsingService_ParsingProgressChanged;
            base.Destroy();
        }
    }
}
