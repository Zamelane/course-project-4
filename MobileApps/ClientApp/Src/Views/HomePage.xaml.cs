using System.Diagnostics;
using ClientApp.Src.Popups;
using CommunityToolkit.Maui.Views;

namespace ClientApp.Src.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        DisplayPopup();
    }

    public async Task DisplayPopup()
    {
        var popup = new QuestionPopup("Тестовый заголовок", "Описание :)");

        var result = await this.ShowPopupAsync(popup, CancellationToken.None);

        if (result is bool boolResult)
        {
            Debug.WriteLine(boolResult);
        }
    }
}