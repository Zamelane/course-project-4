using ClientApp.Src.Popups;

namespace ClientApp.Src.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        var popup = new QuestionPopup();

        popup.CanBeDismissedByTappingOutsideOfPopup = false;

        //this.ShowPopupAsync(popup);
    }
}