namespace ClientApp.Src.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();

#if ANDROID
		Title = "��� �������";
#endif
	}
}