using RequestsLibrary;
using System.Diagnostics;

namespace ClientApp.Src.Utils;
public static class ImageUploader
{
    public static async Task<RequestsLibrary.Models.Image?> SelectAndValidateImageAsync()
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
                return null;
            }

            bool isSuccessful = false;
            using (var stream = await imageFile.OpenReadAsync())
            {
                StreamContent streamContent = new StreamContent(stream);
                RequestParams rp = new();

                MultipartFormDataContent content = new();
                content.Add(streamContent, "image", "test.png");

                rp.SetBody(content);
                var result = await Auxiliary.RunWithStateHandling<RequestsLibrary.Models.Image>(
                    async () => await Fetcher.Fetch<RequestsLibrary.Models.Image>(HttpMethod.Post, Fetcher.Config.GetApiUrl("images"), rp),
                    null,
                    null,
                    r =>
                    {
                        if (r != null)
                            isSuccessful = true;
                    }
                );

                if (isSuccessful)
                {
                    var cover = result.Content;
                    Debug.WriteLine("Загрузил изображение: " + cover?.TotalPath);
                    return cover;
                }
            }

            if (!isSuccessful)
                await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить изображение на сервер", "OK");
        }

        return null;
    }


    private static async Task<FileResult> PickImageAsync()
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

    private static async Task<bool> ValidateImageSizeAsync(FileResult fileResult, long maxSizeInBytes)
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