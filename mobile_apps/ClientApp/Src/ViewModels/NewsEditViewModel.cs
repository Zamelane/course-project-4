using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Diagnostics;
using System.Net.Http;

namespace ClientApp.Src.ViewModels;
public partial class NewsEditViewModel : ObservableObject
{
    [ObservableProperty] public RequestsLibrary.Models.Image cover;

    [RelayCommand]
    private async Task OpenMenu()
    {
        await Shell.Current.CurrentPage.DisplayActionSheet(
            "",
            "",
            "",
            "Сохранить",
            "Опубликовать",
            "Скрыть/Показать обложку"
        );
    }

    [RelayCommand]
    public async Task SelectAndValidateImageAsync()
    {
        // Выбираем изображение
        var imageFile = await PickImageAsync();

        if (imageFile != null)
        {
            // Максимальный размер изображения (например, 2 МБ)
            long maxSizeInBytes = 5 * 1024 * 1024; // 2 МБ

            // Проверяем размер изображения
            bool isValidSize = await ValidateImageSizeAsync(imageFile, maxSizeInBytes);

            if (!isValidSize)
            {
                Debug.WriteLine("Валидация не прошла");
                return;
            }

            bool isSuccessful = false;
            using (var stream = await imageFile.OpenReadAsync())
            {
                StreamContent streamContent = new StreamContent(stream);
                RequestParams rp = new();

                MultipartFormDataContent content = new();
                content.Add(streamContent, "image", imageFile.FileName);

                rp.SetBody(content);
                var result = await Auxiliary.RunWithStateHandling<RequestsLibrary.Models.Image>(
                    async () => await Fetcher.Fetch<RequestsLibrary.Models.Image>(HttpMethod.Post, Fetcher.Config.GetApiUrl("images"), rp),
                    null,
                    null,
                    r =>
                    {
                        if (r != null)
                        {
                            isSuccessful = true;
                            Cover = r;
                        }
                    }
                );
            }

            if (!isSuccessful)
                await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить изображение на сервер", "OK");
        }
    }


    public async Task<FileResult> PickImageAsync()
    {
        try
        {
            // Выбор файла с ограничением на тип (изображения)
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Выберите изображение",
                FileTypes = FilePickerFileType.Images // Ограничение на выбор только изображений
            });

            if (result != null)
            {
                // Проверка расширения файла
                if (result.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    Debug.WriteLine("Изображение выбрано: " + result.FileName);
                    return result;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Выберите файл в формате JPG или PNG.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Ошибка при выборе изображения: " + ex.Message);
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось выбрать изображение.", "OK");
        }

        return null;
    }

    public async Task<bool> ValidateImageSizeAsync(FileResult fileResult, long maxSizeInBytes)
    {
        if (fileResult == null)
            return false;

        try
        {
            // Получаем поток файла
            using var stream = await fileResult.OpenReadAsync();

            // Проверяем размер файла
            if (stream.Length > maxSizeInBytes)
            {
                await Shell.Current.DisplayAlert("Ошибка", $"Размер изображения превышает {maxSizeInBytes / 1024} КБ.", "OK");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Ошибка при проверке размера изображения: " + ex.Message);
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось проверить размер изображения.", "OK");
            return false;
        }
    }
}