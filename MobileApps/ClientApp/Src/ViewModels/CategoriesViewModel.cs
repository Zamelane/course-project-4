﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Responses.Api.Category;
using RequestsLibrary.Routes.API;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels
{
    public partial class CategoriesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CategoryResponse>? categories;
        [ObservableProperty]
        private string? error = null;
        [ObservableProperty]
        private bool isFetching = false;

        public CategoriesViewModel()
        {
            Task.Run(TryFetchCategories);
        }

        [RelayCommand]
        private async Task TryFetchCategories() => await Fetch();
        public async Task<ObservableCollection<CategoryResponse>?> Fetch()
        {
            try
            {
                IsFetching = true;
                Debug.WriteLine("Start TryFetchCategories"); // TODO: DEBUG
                (var response, var body, var exception) = await RequestsLibrary.Routes.API.Categories.GetAllRequest.RequestToServer();

                if (body is null && exception is null)
                    exception = new RequestsLibrary.Exceptions.RequestException("Сервер не вернул данные: " + response.StatusCode);

                if (exception is not null)
                {
                    Error = exception.Message;
                    Debug.WriteLine("Error TryFetchCategories: " + Error); // TODO: DEBUG
                    return null;
                }

                foreach (CategoryResponse cr in body!)
                {
                    cr.AccentColor = $"#{cr.AccentColor}";
                    cr.BackgroundColor = $"#{cr.BackgroundColor}";
                    if (cr.Image is not null)
                        cr.Image.Path = $"{Fetcher.URL}{cr.Image.Path}";
                }

                Categories = body;

                Debug.WriteLine("End TryFetchCategories"); // TODO: DEBUG
            }
            finally
            {
                IsFetching = false;
            }
            
            return Categories;
        }
    }
}