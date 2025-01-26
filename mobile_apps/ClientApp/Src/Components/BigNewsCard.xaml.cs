using ClientApp.Src.Views;

namespace ClientApp.Src.Components;

public partial class BigNewsCard : ContentView
{
    public BigNewsCard()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        // TODO: сделать для большой новости
        //Shell.Current.Navigation.PushAsync(new NewsPage());
    }
}