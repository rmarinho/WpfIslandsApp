using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Markup;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfIslandsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
    }

    internal class XamlApp : Microsoft.UI.Xaml.Application, IXamlMetadataProvider
	{
		public XamlApp()
		{
			_xamlMetaDataProvider = new Microsoft.UI.Xaml.XamlTypeInfo.XamlControlsXamlMetaDataProvider();
			_windowsXamlManager = WindowsXamlManager.InitializeForCurrentThread();
		}

		override protected void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
		{
			this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.Controls.XamlControlsResources());
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

		WindowsXamlManager _windowsXamlManager;
		IXamlMetadataProvider _xamlMetaDataProvider;
	}


}
