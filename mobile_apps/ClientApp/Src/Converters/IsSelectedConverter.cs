using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace ClientApp.Src.Converters;

public class IsSelectedConverter : IValueConverter
{
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        Debug.WriteLine("----");
        Debug.WriteLine(value);
        Debug.WriteLine(parameter);
        if (value is ObservableCollection<object> selectedElements && parameter is not null)
        {
            return selectedElements.Contains(parameter);
        }

        try
        {
            Debug.WriteLine(JsonSerializer.Serialize(value));
            Debug.WriteLine(JsonSerializer.Serialize(parameter));
        }
        catch { }

        return false;
    }

    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}