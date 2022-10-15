using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Drawing;
using Wallpaper_Carousel.Utilities;
using Wallpaper_Carousel.ViewModels;

namespace Wallpaper_Carousel.Controls.SettingControls
{
    public sealed partial class SettingItems : UserControl
    {
        public SettingsViewModel SettingsViewModel => App.ViewModel.SettingsViewModel;

        public SettingItems()
        {
            InitializeComponent();
        }

        public static int GetThemeRadioButtonsSelectedIndex(ElementTheme theme)
        {
            return theme switch
            {
                ElementTheme.Light => 0,
                ElementTheme.Dark => 1,
                _ => 2
            };
        }

        private void OnThemeRadioButtonsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SettingsViewModel.AppTheme = (sender as RadioButtons).SelectedIndex switch
            {
                0 => ElementTheme.Light,
                1 => ElementTheme.Dark,
                _ => ElementTheme.Default
            };
        }

        public static string GetAutoSaveImageStateString(bool flag)
        {
            return flag ? "自动保存" : "手动保存";
        }

        private async void BrowseImageSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            if (await Common.PickSingleFolderAsync() is string folder)
                SettingsViewModel.SaveImagePath = folder;
        }

        public static int GetImageResolutionComboxSelectedIndex(Size resolution)
        {
            return resolution.Height switch
            {
                1080 => 0,
                1440 => 1,
                2160 => 2,
                _ => 3
            };
        }

        private void OnImageResolutionComboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combox = sender as ComboBox;
            SettingsViewModel.ImageResolution = combox.SelectedIndex switch
            {
                0 => new(1920, 1080),
                1 => new(2560, 1440),
                2 => new(3840, 2160),
                _ => new(-1, -1),
            };
        }
    }
}
