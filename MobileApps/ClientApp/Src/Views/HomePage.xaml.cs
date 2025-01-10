using ClientApp.Src.Popups;
using ClientApp.Src.ViewModels;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;

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