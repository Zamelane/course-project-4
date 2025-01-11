using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientApp.Src.Popups.ViewModels;

public partial class QuestionPopupViewModel : ObservableObject
{
    [ObservableProperty] private string description = "Description";
    [ObservableProperty] private QuestionPopup? questionPopup;
    [ObservableProperty] private string title = "Title";

    [RelayCommand]
    private async Task YesButtonClicked()
    {
        await ReturnPopupResult(true);
    }

    [RelayCommand]
    private async Task NoButtonClicked()
    {
        await ReturnPopupResult(false);
    }

    private async Task ReturnPopupResult(bool returnedValue)
    {
        if (QuestionPopup is null)
            return;

        await QuestionPopup.CloseAsync(returnedValue);
    }
}