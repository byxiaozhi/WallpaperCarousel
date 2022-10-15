﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;

namespace Wallpaper_Carousel.Controls.SettingControls
{
    [ContentProperty(Name = nameof(Action))]
    public sealed partial class NormalSettingItem : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(object), typeof(NormalSettingItem), null);
        public object Title { get => GetValue(TitleProperty); set => SetValue(TitleProperty, value); }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof(Description), typeof(object), typeof(NormalSettingItem), null);
        public object Description { get => GetValue(DescriptionProperty); set => SetValue(DescriptionProperty, value); }

        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register(nameof(Action), typeof(object), typeof(NormalSettingItem), null);
        public object Action { get => GetValue(ActionProperty); set => SetValue(ActionProperty, value); }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(object), typeof(IconElement), null);
        public IconElement Icon { get => (IconElement)GetValue(IconProperty); set => SetValue(IconProperty, value); }

        public NormalSettingItem()
        {
            InitializeComponent();
        }
    }
}
