using Microsoft.UI.Xaml;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Wallpaper_Carousel.Utilities;
using Windows.Storage;

namespace Wallpaper_Carousel.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public bool IsAutoRun { get => GetValue(false); set => SetValue(value); }

        public bool IsHideSystemTrayIcon { get => GetValue(false); set => SetValue(value); }

        public bool AutoSaveImage { get => GetValue(false); set => SetValue(value); }

        public Size ImageResolution { get => GetValue(new Size(1920, 1080).PackSize()).UnpackSize(); set => SetValue(value.PackSize()); }

        public string SaveImagePath { get => GetValue(KnownFolders.SavedPictures.Path); set => SetValue(value); }

        public ElementTheme AppTheme { get => (ElementTheme)GetValue((int)ElementTheme.Dark); set => SetValue((int)value); }

        protected T GetValue<T>(T defaultValue, [CallerMemberName] string propertyName = null) => ApplicationData.Current.LocalSettings.Values[propertyName] is T value ? value : defaultValue;

        protected void SetValue(object value, [CallerMemberName] string propertyName = null) => ApplicationData.Current.LocalSettings.Values[propertyName] = value;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
