﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Wallpaper_Carousel.Controls.SettingControls
{
    [ContentProperty(Name = nameof(PrimaryContent))]
    public sealed partial class ResponsiveContainer : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(ResponsiveContainer), null);
        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        public static readonly DependencyProperty PrimaryContentProperty = DependencyProperty.Register(nameof(PrimaryContent), typeof(object), typeof(ResponsiveContainer), null);
        public object PrimaryContent { get => GetValue(PrimaryContentProperty); set => SetValue(PrimaryContentProperty, value); }

        public static readonly DependencyProperty SecondaryContentProperty = DependencyProperty.Register(nameof(SecondaryContent), typeof(object), typeof(ResponsiveContainer), null);
        public object SecondaryContent { get => GetValue(SecondaryContentProperty); set => SetValue(SecondaryContentProperty, value); }

        public ResponsiveContainer()
        {
            InitializeComponent();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Reflow();
        }

        private void Reflow()
        {
            var largeWidth = 1008;
            var mediumWidth = 641;

            Grid_Wrapper.Margin = new(ActualWidth >= mediumWidth ? 48 : 16);
            Grid_Wrapper.ColumnSpacing = ActualWidth >= largeWidth ? 48 : 0;
            Grid_Wrapper.RowSpacing = ActualWidth >= mediumWidth ? 20 : 16;
            TextBlock_Title.FontSize = (double)(ActualWidth >= mediumWidth ? App.Current.Resources["TitleLargeTextBlockFontSize"] : App.Current.Resources["TitleTextBlockFontSize"]);

            if (ActualWidth >= largeWidth)
            {
                Grid.SetRow(ContentPresenter_Primary, 1);
                Grid.SetRow(ContentPresenter_Secondary, 1);
                Grid.SetColumn(ContentPresenter_Primary, 0);
                Grid.SetColumn(ContentPresenter_Secondary, 1);
            }
            else
            {
                Grid.SetRow(ContentPresenter_Primary, 1);
                Grid.SetRow(ContentPresenter_Secondary, 2);
                Grid.SetColumn(ContentPresenter_Primary, 0);
                Grid.SetColumn(ContentPresenter_Secondary, 0);
            }
        }
    }
}
