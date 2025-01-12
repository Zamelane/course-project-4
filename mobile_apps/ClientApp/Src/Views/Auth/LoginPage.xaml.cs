using ClientApp.Src.ViewModels.Auth;

namespace ClientApp.Src.Views.Auth;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void FocusToPassword(object sender, EventArgs e)
    {
        PasswordEntry.Focus();
    }

    private void GoToSignupCommand(object sender, EventArgs e)
    {
        if (BindingContext is LoginViewModel vm)
            vm.TryLoginCommand.Execute(null);
    }
}