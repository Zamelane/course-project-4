using RequestsLibrary;

namespace ClientApp.Src.Utils;

internal static class Auxiliary
{

    public static readonly List<string> MonthNames = [
        "Январь",
        "Февраль",
        "Март",
        "Март",
        "Апрель",
        "Май",
        "Июнь",
        "Июль",
        "Август",
        "Сентябрь",
        "Октябрь",
        "Ноябрь"
    ];
    public static T? GetRootLayout<T>(Element currentElement)
    {
        try
        {
            var parentElement = currentElement.Parent;

            if (parentElement is T rl)
                return rl;
            return GetRootLayout<T>(parentElement);
        }
        catch
        {
            return default;
        }
    }

    public static async Task<Response<T?>> RunWithStateHandling<T>(Func<Task<Response<T?>>> fetchMathod, Action<bool>? setIsFetching = null, Action<string>? setError = null, Action<T?>? setResult = null)
    {
        setIsFetching?.Invoke(true);
        Response<T?> response = await fetchMathod();
        setIsFetching?.Invoke(false);

        string error = "";

        if (response.IsEmpty)
            error = "Сервер не вернул данные: " + response.StatusCode;

        if (!response.IsEmptyError)
            error = response.Error!.Comment!;

        setError?.Invoke(error);
        setResult?.Invoke(response.Content);

        return response;
    }

    public static async Task<T?> RunWithStateHandlingWithoutResponse<T>(Func<Task<T?>> fetchMathod, Action<bool>? setIsFetching = null, Action<string>? setError = null, Action<T?>? setResult = null)
    {
        setIsFetching?.Invoke(true);
        T? response = await fetchMathod();
        setIsFetching?.Invoke(false);

        string error = "";

        if (response is null)
            error = "Нечего возвращать";

        setError?.Invoke(error);
        setResult?.Invoke(response);

        return response;
    }
    public static string FormateDate(DateTime date)
    {
        return $"{MonthNames[date.Month - 1].Substring(0, 3)} {date.Day}, {date.Year}";
    }
    public static string SymbolsToReadTime(int symbols)
    {
        // Сколько символов в минуту читаем
        int symbolsInSeconds = 14;

        List<string> units = ["секунд", "минут", "часов", "дней", "месяцев", "лет"];
        List<int>   limits = [60, 60, 24, 31, 12, 1000];

        int unit = 0;
        int currentTime = symbols / symbolsInSeconds;

        while (currentTime > limits[unit] && limits.Count - 1 > unit)
        {
            currentTime /= limits[unit];
            unit++;
        }

        //return $"{currentTime / limits[unit]} {units[unit]} чтения";
        return $"{currentTime} {units[unit]} чтения";
    }
}