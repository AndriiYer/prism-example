using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Yermolchev.Assignment.Modules.TextTool;
using Yermolchev.Assignment.Services;
using Yermolchev.Assignment.Services.Interfaces;
using Yermolchev.Assignment.Views;

namespace Yermolchev.Assignment
{
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
            containerRegistry.RegisterSingleton<ITextParsingService, TextParsingService>();
            containerRegistry.RegisterSingleton<IStreamReaderProvider, FileStreamReaderProvider>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<TextToolModule>();
        }
    }
}
