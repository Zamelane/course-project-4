using System.Diagnostics;
using System.Globalization;

namespace ClientApp.Src.Converters;

public class VisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        if ((value is string s
             && s == string.Empty)
            || value is null
           )
            return false;
        return true;
    }

    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}