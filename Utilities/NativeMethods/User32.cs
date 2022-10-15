using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper_Carousel.Utilities.NativeMethods
{
    public class User32
    {
        [DllImport("user32.dll")]
        public static extern int GetDpiForWindow(IntPtr hWnd);

        public static double GetScaleForWindow(IntPtr hWnd) => GetDpiForWindow(hWnd) / 96d;
    }
}
