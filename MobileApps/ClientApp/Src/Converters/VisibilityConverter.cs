using System.Globalization;

namespace ClientApp.Src.Converters;
public class VisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        if (value is string s
            && s == String.Empty
        )
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}
