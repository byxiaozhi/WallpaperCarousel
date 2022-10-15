using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Wallpaper_Carousel.ViewModels;
using WinRT.Interop;

namespace Wallpaper_Carousel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();
            ViewModel = new AppViewModel();
            ResizeWindow();
            CenterWindow();
            Window.Activate();
        }

        private void ResizeWindow()
        {
            var width = 1500;
            var height = 900;
            var hWnd = WindowNative.GetWindowHandle(Window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            var scale = Utilities.NativeMethods.User32.GetScaleForWindow(hWnd);
            appWindow.Resize(new((int)(width * scale), (int)(height * scale)));
        }

        private void CenterWindow()
        {
            var hWnd = WindowNative.GetWindowHandle(Window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            if (appWindow is not null)
            {
                var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Nearest);
                if (displayArea is not null)
                {
                    var CenteredPosition = appWindow.Position;
                    CenteredPosition.X = ((displayArea.WorkArea.Width - appWindow.Size.Width) / 2);
                    CenteredPosition.Y = ((displayArea.WorkArea.Height - appWindow.Size.Height) / 2);
                    appWindow.Move(CenteredPosition);
                }
            }
        }

        public static Window Window { get; private set; }

        public static AppViewModel ViewModel { get; private set; }
    }
}
