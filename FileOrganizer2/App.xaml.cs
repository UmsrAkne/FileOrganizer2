using Prism.Ioc;
using FileOrganizer2.Views;
using System.Windows;
using FileOrganizer2.Models;

namespace FileOrganizer2
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
            #if DEBUG
            containerRegistry.Register<IFileProvider, DummyFileProvider>();
            #endif
            #if RELEASE
            containerRegistry.Register<IFileProvider, FileProvider>();
            #endif
        }
    }
}