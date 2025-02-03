using RequestsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Src.Converters;
public class ResultConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        //Debug.WriteLine($"Value: {value}, {parameter}");

        if (value is RequestsLibrary.Models.Image i)
            value = String.IsNullOrEmpty(i.Url) ? null : i.TotalPath;

        Debug.WriteLine(value);

        if ((value is string s
             && s == string.Empty)
            || value is null
           )
            return parameter;
        return value;
    }

    public object ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        throw new NotImplementedException();
    }
}