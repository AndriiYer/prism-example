using Prism.Mvvm;

namespace Yermolchev.Assignment.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Text tool";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
