using System;
using System.Globalization;
using System.Windows.Data;
using ApplicationLogic.Model;
using ApplicationLogic.TypeConverter;

namespace WPF_UI.Converters
{
    [ValueConversion(typeof(object), typeof(string))]
    public class ItemTypeToString : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TypeToStringConverter.GetStringFromType(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
