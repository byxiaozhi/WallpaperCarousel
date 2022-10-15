using Microsoft.UI.Xaml.Controls;
using Wallpaper_Carousel.Utilities;

namespace Wallpaper_Carousel.Controls
{
    public sealed partial class AppTitleBar : UserControl
    {
        public AppTitleBar()
        {
            InitializeComponent();
            TextBlock_AppName.Text = Common.GetLocalizedString("AppName");
        }
    }
}
