namespace ClientApp.Src.Utils;

internal static class Auxiliary
{
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

    public static void Wait(Task t)
    {
        t.Wait();
    }
}