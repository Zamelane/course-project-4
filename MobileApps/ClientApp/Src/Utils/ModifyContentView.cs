using System.Diagnostics;

namespace ClientApp.Src.Utils;

public class ModifyContentView : ContentView
{
    public T? GetTemplateElement<T>(string name)
    {
        try
        {
            var templateElement = GetTemplateChild(name);

            if (templateElement is T e)
                return e;
        }
        catch
        {
            Debug.WriteLine($"{ToString()}: Template element not found");
        }

        return default;
    }
}