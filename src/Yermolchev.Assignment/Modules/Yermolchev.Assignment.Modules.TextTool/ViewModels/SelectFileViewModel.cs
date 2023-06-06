using System.IO;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using Yermolchev.Assignment.Core;
using Yermolchev.Assignment.Core.Mvvm;
using Yermolchev.Assignment.Modules.TextTool.Views;

namespace Yermolchev.Assignment.Modules.TextTool.ViewModels
{
    public class SelectFileViewModel : RegionViewModelBase
    {
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        private DelegateCommand _openFileDialogCommand;
        public DelegateCommand OpenFileDialogCommand =>
            _openFileDialogCommand ??= new DelegateCommand(OpenFileDialog);

        private DelegateCommand _processFileCommand;
        public DelegateCommand ProcessFileCommand =>
            _processFileCommand ??= new DelegateCommand(ProcessText, CanProcessText);

        public SelectFileViewModel(IRegionManager regionManager) :
            base(regionManager) { }

        private bool CanProcessText() => File.Exists(FileName);

        private void ProcessText()
        {
            var parameters = new NavigationParameters
            {
                { nameof(FileName), FileName }
            };

            RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(ProcessFile), parameters);
        }

        private void OpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                FileName = openFileDialog.FileName;
                ProcessFileCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
