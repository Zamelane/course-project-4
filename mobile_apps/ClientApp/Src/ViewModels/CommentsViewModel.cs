using ClientApp.Src.Utils;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class CommentsViewModel : ObservableObject
{
    [ObservableProperty] public int newsId;
    [ObservableProperty] public ObservableCollection<Comment> comments = [];
    [ObservableProperty] public bool isEndPage = false;
    [ObservableProperty] public bool isCommentsFetching = false;
    private int _currentPage = 0;


    [RelayCommand]
    private async Task FetchMoreNews(int? page = null)
    {
        page ??= _currentPage;

        if (page == 0)
            IsEndPage = false;

        if (IsEndPage || IsCommentsFetching)
            return;

        Debug.WriteLine("Загрузка ещё комментариев ...");

        page++; // Загружаем сразу следующую

        // Строим параметры запроса
        RequestParams rp = new();
        rp.AddParameter("page", page);

        // Запрашиваем данные
        var response = await Auxiliary.RunWithStateHandling(
            async () => await Fetcher.Fetch<ObservableCollection<Comment>>(HttpMethod.Get, Fetcher.Config.GetApiUrl($"news/{NewsId}/comments")),
            _ => IsCommentsFetching = _,
            _ =>
            {
                Toast.Make(_).Show();
                Debug.WriteLine(_);
            },
            fn =>
            {
                if (fn is null)
                    return;
                if (page != 1)
                    fn.ToList().ForEach(Comments.Add);
                else Comments = fn;
            }
        );

        // Прверяем, есть ли ошибки и пришло ли тело
        if (!response.IsEmptyError || response.IsEmptyContent)
            return;

        // Если не вернулись элементы, то не загружаем следующие страницы
        if (response.Content?.Count == 0)
        {
            IsEndPage = true;
            return;
        }

        // Если ошибок нет и ответ не пустой, то запоминаем текущую страницу
        _currentPage = (int)page;
    }

}