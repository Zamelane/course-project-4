namespace ClientApp.Src.Views.Auth;

public partial class SignupPage : ContentPage
{
    public SignupPage()
    {
        InitializeComponent();
    }

    private void FocusToLastName(object sender, EventArgs e)
    {
        LastName.Focus();
    }

    private void FocusToLogin(object sender, EventArgs e)
    {
        Login.Focus();
    }

    private void FocusToEmail(object sender, EventArgs e)
    {
        Email.Focus();
    }

    private void FocusToPassword(object sender, EventArgs e)
    {
        Password.Focus();
    }

    private void FocusToPasswordConfirm(object sender, EventArgs e)
    {
        PasswordConfirm.Focus();
    }
}