using System.Diagnostics;
using System.Globalization;

namespace ClientApp.Src.Utils;
public class ErrorConverter : IValueConverter
{
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        Debug.WriteLine(value is Dictionary<string, List<string>> d ? d.Count : "None");
        Debug.WriteLine(parameter);
        if (value is Dictionary<string, List<string>> errors
            && parameter is string key
            && errors.ContainsKey(key)
        )
        {
            return string.Join("\n", errors[key]);
        }
        return string.Empty;
    }

    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}