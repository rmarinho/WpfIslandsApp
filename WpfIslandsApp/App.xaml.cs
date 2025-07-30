using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Markup;
using System.Configuration;
using System.Data;
using System.Resources;
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
			this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.ResourceDictionary
				{
					Source = new Uri("ms-appx:///Microsoft.Maui/Platform/Windows/Styles/Resources.xbf")
				});

			//this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.ResourceDictionary
			//	{
			//		Source = new Uri("ms-appx:///Platform/Windows/CollectionView/ItemsViewStyles.xbf")
			//	});

        }

		IXamlType IXamlMetadataProvider.GetXamlType(string fullName)
		{
			var xamlType = _xamlMetaDataProvider.GetXamlType(fullName);
            return xamlType;
		}

		IXamlType IXamlMetadataProvider.GetXamlType(System.Type type)
		{
			var xamlType = _xamlMetaDataProvider.GetXamlType(type);
            return xamlType;
		}

		XmlnsDefinition[] IXamlMetadataProvider.GetXmlnsDefinitions()
		{
			return _xamlMetaDataProvider.GetXmlnsDefinitions();
		}

		WindowsXamlManager _windowsXamlManager;
		IXamlMetadataProvider _xamlMetaDataProvider;
	}


}
