namespace ClientApp.Src.Utils;
public partial class ModifyContentView : ContentView
{
    public T? GetTemplateElement<T>(string name)
    {
        try
        {
            var templateElement = this.GetTemplateChild(name);

            if (templateElement is T e)
                return e;

            return default;
        }
        catch
        {
            return default;
        }
    }
}
