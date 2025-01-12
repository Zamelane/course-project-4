using System.Text.Json;
using RequestsLibrary.RequestsData.ComponentsRequests;

namespace ClientApp.Src.Storage;

public static class AuthData
{
    private static string? _token;
    private static User? _user;

    public static string? Token
    {
        get => _token ??= SecureStorage.GetAsync("token").Result;
        set
        {
            _token = value;

            if (value == null)
                SecureStorage.Remove("token");
            else SecureStorage.SetAsync("token", value);
        }
    }

    public static User? User
    {
        get
        {
            if (_user == null)
            {
                var fromSettings = Preferences.Get("user", null);
                if (fromSettings != null)
                    _user = JsonSerializer.Deserialize<User>(fromSettings);
            }

            return _user;
        }
        set
        {
            _user = value;

            if (value == null)
                Preferences.Remove("user");
            else
                Preferences.Set("user", JsonSerializer.Serialize(value));
        }
    }

    public static void SaveAuthData(string token, User user)
    {
        Token = token;
        User = user;
    }
}