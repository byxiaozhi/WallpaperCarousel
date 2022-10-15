using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using Wallpaper_Carousel.Controls;
using Wallpaper_Carousel.Utilities;
using WinRT.Interop;

namespace Wallpaper_Carousel
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = Common.GetLocalizedString("AppName");
            InitializeComponent();
            InitializeTitleBar();
        }

        private void InitializeTitleBar()
        {
            var appWindow = GetAppWindowForCurrentWindow();
            if (Microsoft.UI.Windowing.AppWindowTitleBar.IsCustomizationSupported())
            {
                appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                UpdateTitleBarButtonTheme();
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
                RootContainer.Background = RootContainer.Resources["BackgroundFallbackColorBrush"] as Brush;
            }
        }

        private void UpdateTitleBarButtonTheme()
        {
            if (Microsoft.UI.Windowing.AppWindowTitleBar.IsCustomizationSupported())
            {
                var appWindow = GetAppWindowForCurrentWindow();
                appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

                switch (AppTitleBar.ActualTheme)
                {
                    case ElementTheme.Light:
                        appWindow.TitleBar.ButtonInactiveForegroundColor = Common.GetColor("#9b9b9b");
                        appWindow.TitleBar.ButtonForegroundColor = Common.GetColor("#171717");
                        appWindow.TitleBar.ButtonHoverForegroundColor = Common.GetColor("#000000");
                        appWindow.TitleBar.ButtonPressedForegroundColor = Common.GetColor("#5f5f5f");
                        appWindow.TitleBar.ButtonHoverBackgroundColor = Common.GetColor("#e9e9e9");
                        appWindow.TitleBar.ButtonPressedBackgroundColor = Common.GetColor("#ededed");
                        break;
                    case ElementTheme.Dark:
                        appWindow.TitleBar.ButtonInactiveForegroundColor = Common.GetColor("#737373");
                        appWindow.TitleBar.ButtonForegroundColor = Common.GetColor("#f2f2f2");
                        appWindow.TitleBar.ButtonHoverForegroundColor = Common.GetColor("#ffffff");
                        appWindow.TitleBar.ButtonPressedForegroundColor = Common.GetColor("#a7a7a7");
                        appWindow.TitleBar.ButtonHoverBackgroundColor = Common.GetColor("#2d2d2d");
                        appWindow.TitleBar.ButtonPressedBackgroundColor = Common.GetColor("#292929");
                        break;
                    default:
                        break;
                }
            }
        }

        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(wndId);
        }

        private void AppTitleBar_ActualThemeChanged(FrameworkElement sender, object args)
        {
            UpdateTitleBarButtonTheme();
        }
    }
}
