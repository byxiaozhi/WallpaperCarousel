using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using Wallpaper_Carousel.Pages;

namespace Wallpaper_Carousel.Controls
{
    public sealed partial class RootNavigation : UserControl
    {
        public RootNavigation()
        {
            InitializeComponent();
            NavigationView.SelectedItem = NavigationView.MenuItems.First();
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var pageName = (sender.SelectedItem as NavigationViewItem).Tag as string;
            ContentFrame.Navigate(Route(pageName), null, args.RecommendedNavigationTransitionInfo);
        }

        private static Type Route(string name)
        {
            return name switch
            {
                "Gallery" => typeof(Gallery),
                "Carousel" => typeof(Carousel),
                _ => typeof(Settings)
            };
        }
    }
}
