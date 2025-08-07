using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Application = Microsoft.UI.Xaml.Application;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiLib
{
    public partial class XamlApp : Application, IXamlMetadataProvider
    {
        WindowsXamlManager _windowsXamlManager;
        IXamlMetadataProvider _xamlMetaDataProvider;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public XamlApp(IXamlMetadataProvider provider)
        {
            this.ResourceManagerRequested += App_ResourceManagerRequested;

            _xamlMetaDataProvider = provider;

            _windowsXamlManager = WindowsXamlManager.InitializeForCurrentThread();

            //InitializeComponent();
        }

        private void App_ResourceManagerRequested(object sender, ResourceManagerRequestedEventArgs args)
        {
            args.CustomResourceManager = new Microsoft.Windows.ApplicationModel.Resources.ResourceManager("Microsoft.Maui.Controls.pri");
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
          //  this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.Controls.XamlControlsResources());
            //this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.ResourceDictionary
            //{
            //    Source = new Uri("ms-appx:///Microsoft.Maui/Platform/Windows/Styles/Resources.xbf")
            //});

        }

        IXamlType IXamlMetadataProvider.GetXamlType(string fullName)
        {
            return _xamlMetaDataProvider.GetXamlType(fullName);
        }

        IXamlType IXamlMetadataProvider.GetXamlType(System.Type type)
        {
            return _xamlMetaDataProvider.GetXamlType(type);
        }

        XmlnsDefinition[] IXamlMetadataProvider.GetXmlnsDefinitions()
        {
            return _xamlMetaDataProvider.GetXmlnsDefinitions();
        }
    }
}
