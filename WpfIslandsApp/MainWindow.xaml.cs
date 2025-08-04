using MauiLib;
using Microsoft.Maui.Controls.Embedding;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Markup;
using System.Windows;

namespace WpfIslandsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        DispatcherQueueController _controller;

        private WinUIControlHost _winUIControl;
        private System.Windows.Window _myWindow;
    
        public MainWindow()
        {
            _controller = Microsoft.UI.Dispatching.DispatcherQueueController.CreateOnCurrentThread();

            var xamlApp = new XamlApp();

            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _winUIControl = new WinUIControlHost(ControlHostElement.ActualHeight, ControlHostElement.ActualWidth);
            ControlHostElement.Child = _winUIControl;
        }
    }
}