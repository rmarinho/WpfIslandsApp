using MauiLib;
using Microsoft.Maui.Controls.Embedding;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using Microsoft.UI.Content;
using Microsoft.UI.Xaml.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace WpfIslandsApp
{
    public class WinUIControlHost : HwndHost
    {
        int hostHeight, hostWidth;

        Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource _xamlSource;


        public WinUIControlHost(double height, double width)
        {
            hostHeight = (int)height;
            hostWidth = (int)width;
            _xamlSource = new Microsoft.UI.Xaml.Hosting.DesktopWindowXamlSource();
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            var id = new Microsoft.UI.WindowId((ulong)hwndParent.Handle);
            _xamlSource.Initialize(id);

            InitMauiSampleCode();
            //_xamlSource.SiteBridge.ResizePolicy = Microsoft.UI.Content.ContentSizePolicy.ResizeContentToParentWindow;
            //_xamlSource.SiteBridge.Show();

            return new HandleRef(null, (nint)_xamlSource.SiteBridge.WindowId.Value);
        }

        public void InitMauiSampleCode()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbeddedApp<MauiLib.App>();
            MauiApp mauiApp = builder.Build();

            var _mauiContext = new MauiContext(mauiApp.Services);

            _xamlSource.Content = new CustomView().ToPlatform(_mauiContext);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
           
        }
    }
}
