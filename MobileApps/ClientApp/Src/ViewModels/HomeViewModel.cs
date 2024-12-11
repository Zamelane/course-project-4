using ClientApp.Src.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ClientApp.Src.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<News> newses = new ObservableCollection<News>()
    {
        new News()
        {
            Title = "Технологический гигант представил революционное устройство на базе ...",
            Category = "Технологии",
            ImgUrl = "https://proza.ru/pics/2021/12/31/213.jpg",
            CompanyImgUrl = "https://avatars.mds.yandex.net/i?id=930cf37534cf1d2f8e1c6c9a19d5e15a5da26d02-6637353-images-thumbs&n=13",
            CompanyName = "BBC News",
            Date = "9 июня, 2024"
        },
        new News()
        {
            Title = "Технологический гигант представил революционное устройство на базе ...",
            Category = "Технологии",
            ImgUrl = "https://proza.ru/pics/2021/12/31/213.jpg",
            CompanyImgUrl = "https://avatars.mds.yandex.net/i?id=930cf37534cf1d2f8e1c6c9a19d5e15a5da26d02-6637353-images-thumbs&n=13",
            CompanyName = "BBC News",
            Date = "9 июня, 2024"
        },
        new News()
        {
            Title = "Технологический гигант представил революционное устройство на базе ...",
            Category = "Технологии",
            ImgUrl = "https://proza.ru/pics/2021/12/31/213.jpg",
            CompanyImgUrl = "https://avatars.mds.yandex.net/i?id=930cf37534cf1d2f8e1c6c9a19d5e15a5da26d02-6637353-images-thumbs&n=13",
            CompanyName = "BBC News",
            Date = "9 июня, 2024"
        }
    };

    [RelayCommand]
    private async Task GoToLoginPage()
    {
        await Shell.Current.Navigation.PushModalAsync(new Views.Auth.LoginPage());
    }
}