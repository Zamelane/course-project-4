using ClientApp.Src.Components;

namespace ClientApp.Src.Utils;
static class Utils
{
    public static T? GetRootLayout<T>(Element currentElement)
    {
        try
        {
            var parentElement = currentElement.Parent;

            if (parentElement is T rl)
                return rl;
            else return GetRootLayout<T>(parentElement);
        } catch
        {
            return default;
        }
    }
}