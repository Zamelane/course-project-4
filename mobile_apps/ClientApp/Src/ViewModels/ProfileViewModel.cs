using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using RequestsLibrary.Requests;
using System.ComponentModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class ProfileViewModel : ObservableObject
{
    private string _email = "test@mail.ru";
    [ObservableProperty] private User user;
    [ObservableProperty] private string email;
    [ObservableProperty] private Dictionary<string, List<string>> badFields = [];
    [ObservableProperty] private string  password;
    [ObservableProperty] private string  firstName;
    [ObservableProperty] private string  lastName;
    [ObservableProperty] private RequestsLibrary.Models.Image? avatar;
    [ObservableProperty] private string? error;

    [ObservableProperty] private bool isFetching = false;

    public ProfileViewModel()
    {
        Provider.AuthData.PropertyChanged += AuthData_PropertyChanged;
        Debug.WriteLine("User avatar: " + User?.Avatar?.TotalPath);
        Email = Provider.AuthData.User.Email;
        User = Provider.AuthData.User;
        FirstName = Provider.AuthData?.User?.FirstName ?? "";
        LastName = Provider.AuthData?.User?.LastName ?? "";
        Avatar = Provider.AuthData?.User?.Avatar;
    }

    private void AuthData_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthData.User))
        {
            // Обновляем свойство User при изменении AuthData.User
            User = Provider.AuthData.User;

            if (User is not null)
                Email = User.Email;
            else Email = _email;

            OnPropertyChanged(nameof(User));
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        RequestParams rp = new()
        {
            Body = new UpdateProfileRequest(FirstName, LastName, Avatar?.Hash, Password)
        };

        var result = await Auxiliary.RunWithStateHandling(
            async () => await Fetcher.Fetch<User>(HttpMethod.Post, Fetcher.Config.GetApiUrl($"users/{User.Id}/update"), rp),
            _ => IsFetching = _,
            e => Error = e,
            null
        );

        BadFields = [];

        if (!result.IsEmptyError)
            BadFields = result?.Error?.ValidationResponse?.Errors ?? [];
        else
        {
            Provider.AuthData.User.FirstName = FirstName;
            Provider.AuthData.User.LastName = LastName;
            Provider.AuthData.User.Email = Email;
            Provider.AuthData.User.PathAvatar = Avatar?.Url ?? Provider.AuthData.User.PathAvatar;

            Provider.AuthData.Changed();
        }
    }

    [RelayCommand]
    private async Task UploadImg()
    {
        var image = await ImageUploader.SelectAndValidateImageAsync();

        if (image is not null)
        {
            Avatar = image;
            OnPropertyChanged(nameof(Avatar));
        }
    }
}