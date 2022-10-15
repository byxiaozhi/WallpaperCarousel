using Microsoft.UI.Xaml.Data;
using System;

namespace Wallpaper_Carousel.Resources.Converters
{
    public class BoolToObjectConverter : IValueConverter
    {
        public object TrueValue { get; set; }

        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool val && val)
                return TrueValue;
            else
                return FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
