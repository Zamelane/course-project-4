using ClientApp.Src.Popups.ViewModels;
using CommunityToolkit.Maui.Views;

namespace ClientApp.Src.Popups;

public partial class QuestionPopup : Popup
{
    public QuestionPopup(string title, string description)
    {
        InitializeComponent();

        if (BindingContext is not QuestionPopupViewModel vm)
            return;

        vm.Title = title;
        vm.Description = description;
        vm.QuestionPopup = this;
    }
}