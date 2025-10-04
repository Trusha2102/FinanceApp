using System;
using Microsoft.Maui.Controls;

namespace FinanceApp.Converters
{
    public class BoolToBalanceTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? "✅ Balanced" : "⚠️ Not Balanced";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}
