using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUILib
{
    public class WinuiVIew : Microsoft.UI.Xaml.Controls.UserControl
    {

        public WinuiVIew()
        {
            Content = new Microsoft.UI.Xaml.Controls.TextBlock
            {
                Text = "Hello from WinUI!"
            };

        }
    }
}
