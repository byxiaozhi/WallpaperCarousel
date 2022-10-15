using System.Runtime.InteropServices;
using WinRT;
using Microsoft.UI.Xaml;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Runtime.CompilerServices;

namespace Wallpaper_Carousel.Utilities
{
    public static class BackdropMaterial
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DispatcherQueueOptions
        {
            internal int dwSize;
            internal int threadType;
            internal int apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        private static object dispatcherQueueController = null;

        public static void EnsureWindowsSystemDispatcherQueueController()
        {
            if (Windows.System.DispatcherQueue.GetForCurrentThread() == null)
            {
                _ = CreateDispatcherQueueController(new()
                {
                    dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions)),
                    threadType = 2, // DQTYPE_THREAD_CURRENT
                    apartmentType = 2 // DQTAT_COM_STA
                }, ref dispatcherQueueController);
            }
        }

        private record MicaAttachedProperties(MicaController Controller, SystemBackdropConfiguration Configuration);

        private static readonly ConditionalWeakTable<Window, MicaAttachedProperties> windowMicaAttachedProperties = new();

        public static void SetApply(Window window, bool value)
        {
            if (value)
                TrySetMicaBackdrop(window);
            else
                UnsetMicaBackdrop(window);
        }

        public static bool GetApply(Window window)
        {
            return windowMicaAttachedProperties.TryGetValue(window, out _);
        }

        private static void TrySetMicaBackdrop(Window window)
        {
            if (MicaController.IsSupported() && !GetApply(window))
            {
                EnsureWindowsSystemDispatcherQueueController();
                var controller = new MicaController();
                var configuration = new SystemBackdropConfiguration();
                windowMicaAttachedProperties.Add(window, new(controller, configuration));
                configuration.IsInputActive = true;
                controller.AddSystemBackdropTarget(window.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                controller.SetSystemBackdropConfiguration(configuration);
                window.Activated += OnWindowActivated;
                window.Closed += OnWindowClosed;
            }
        }

        private static void UnsetMicaBackdrop(Window window)
        {
            if (windowMicaAttachedProperties.TryGetValue(window, out var properties))
            {
                window.Activated -= OnWindowActivated;
                window.Closed -= OnWindowClosed;
                properties.Controller.Dispose();
                windowMicaAttachedProperties.Remove(window);
            }
        }

        private static void OnWindowActivated(object sender, WindowActivatedEventArgs args)
        {
            if (windowMicaAttachedProperties.TryGetValue(sender as Window, out var properties))
                properties.Configuration.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private static void OnWindowClosed(object sender, WindowEventArgs args)
        {
            UnsetMicaBackdrop(sender as Window);
        }

        public static void SetTheme(Window window, ElementTheme theme)
        {
            if (windowMicaAttachedProperties.TryGetValue(window, out var properties))
                properties.Configuration.Theme = (SystemBackdropTheme)theme;
        }

        public static ElementTheme GetTheme(Window window)
        {
            if (windowMicaAttachedProperties.TryGetValue(window, out var properties))
                return (ElementTheme)properties.Configuration.Theme;
            return ElementTheme.Default;
        }
    }
}
