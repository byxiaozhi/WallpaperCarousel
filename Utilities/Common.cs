using Microsoft.UI.Xaml.Markup;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Wallpaper_Carousel.Utilities
{
    public static class Common
    {
        private static Lazy<ResourceLoader> resourceLoader = new(() => ResourceLoader.GetForViewIndependentUse("Resources"));

        public static string GetLocalizedString(string resource) => resourceLoader.Value.GetString(resource);

        public static Windows.UI.Color GetColor(string str)
        {
            return (Windows.UI.Color)XamlBindingHelper.ConvertValue(typeof(Windows.UI.Color), str);
        }

        public static async Task<string> PickSingleFolderAsync()
        {
            var folderPicker = new FolderPicker();
            var hwnd = WindowNative.GetWindowHandle(App.Window);
            InitializeWithWindow.Initialize(folderPicker, hwnd);
            var folder = await folderPicker.PickSingleFolderAsync();
            return folder?.Path;
        }

        public static long PackSize(this System.Drawing.Size size)
        {
            return ((long)size.Width) << 32 | (uint)size.Height;
        }

        public static System.Drawing.Size UnpackSize(this long value)
        {
            return new((int)(value >> 32), (int)(value & 0xFFFF));
        }
    }
}
