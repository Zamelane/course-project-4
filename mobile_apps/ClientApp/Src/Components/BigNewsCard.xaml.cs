using ClientApp.Src.Views;
using CommunityToolkit.Maui.Views;

namespace ClientApp.Src.Components;

public partial class BigNewsCard : ContentView
{
    public BigNewsCard()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new NewsPage());
    }
}