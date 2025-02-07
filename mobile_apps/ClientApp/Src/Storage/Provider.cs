using ClientApp.Src.ViewModels;

namespace ClientApp.Src.Storage;

public static class Provider
{
    public static AppShell? AppShell;
    public static CategoriesViewModel CategoriesViewModel = new();
    public static AuthData AuthData = new();
}