using MauiLib;
using Microsoft.Maui.Controls.Embedding;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using Microsoft.UI.Dispatching;
using System.Windows;

namespace WpfIslandsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        MauiContext _mauiContext;
        Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource _xamlSource;

        DispatcherQueueController _controller;
        public MainWindow()
        {
            _controller = Microsoft.UI.Dispatching.DispatcherQueueController.CreateOnCurrentThread();

            var xamlApp = new XamlApp();

            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            var wih = new System.Windows.Interop.WindowInteropHelper(this);
            var id = new Microsoft.UI.WindowId((ulong)wih.Handle);
            _xamlSource = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
            _xamlSource.Initialize(id);

            MauiAppBuilder builder = MauiApp.CreateBuilder();
             builder.UseMauiEmbeddedApp<MauiLib.App>();
            MauiApp mauiApp = builder.Build();

            _mauiContext = new MauiContext(mauiApp.Services);

            _xamlSource.Content = new CustomView().ToPlatform(_mauiContext);
            _xamlSource.SiteBridge.ResizePolicy = Microsoft.UI.Content.ContentSizePolicy.ResizeContentToParentWindow;
            _xamlSource.SiteBridge.Show();
        }
    }
}