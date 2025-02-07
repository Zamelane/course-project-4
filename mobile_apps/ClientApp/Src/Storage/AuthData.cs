using CommunityToolkit.Mvvm.ComponentModel;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ClientApp.Src.Storage;

public class AuthData : ObservableObject
{
    private string? _token;
    private User? _user;

    public string? Token
    {
        get => _token ??= SecureStorage.GetAsync("token").Result;
        set
        {
            _token = value;
            Fetcher.SetToken(value);

            if (value == null)
                SecureStorage.Remove("token");
            else SecureStorage.SetAsync("token", value);
        }
    }

    public User? User
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

            OnPropertyChanged(nameof(User));
        }
    }

    public void SaveAuthData(string token, User user)
    {
        Token = token;
        User = user;
    }

    public void Changed()
    {
        OnPropertyChanged(nameof(User));
    }
}