using System.Windows;

namespace WpfIslandsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource _xamlSource = null;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var wih = new System.Windows.Interop.WindowInteropHelper(this);
            var id = new Microsoft.UI.WindowId((ulong)wih.Handle);
            //var controller = Microsoft.UI.Dispatching.DispatcherQueueController.CreateOnDedicatedThread();
            _xamlSource = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
            _xamlSource.Initialize(id);
            _xamlSource.Content = new WinUILib.WinuiVIew();
            _xamlSource.SiteBridge.ResizePolicy = Microsoft.UI.Content.ContentSizePolicy.ResizeContentToParentWindow;
            _xamlSource.SiteBridge.Show();
        }
    }
}