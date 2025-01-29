using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class FavouriteViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<MinNews> news = [];
    [ObservableProperty] private string? error = null;
    [ObservableProperty] private int pageSize = 2;
    [ObservableProperty] private bool isEndPage = false;
    private int page = 0;

    public FavouriteViewModel()
    {
        GetMoreNewsCommand.Execute(null);
    }
    public bool CanGetMoreNews => !IsEndPage;
    [RelayCommand(CanExecute = nameof(CanGetMoreNews))]
    private async Task GetMoreNews()
    {
        if (IsEndPage)
            return;

        page++;

        RequestParams rp = new();
        rp.AddParameter("page", page);

        await Auxiliary.RunWithStateHandling(
            () => Fetcher.Favourite.Get(rp),
            null,
            _ => Error = _,
            r =>
            {
                if (r is null){
                    Debug.WriteLine("Ничего не пришло для избранного");
                    page--;
                    return;
                }

                if (r.Count == 0)
                {
                    Debug.WriteLine("Избранное пустое");
                    IsEndPage = true;
                }
                else
                    r.ToList().ForEach(e => News.Add(e.News));
            }
        );
    }
}