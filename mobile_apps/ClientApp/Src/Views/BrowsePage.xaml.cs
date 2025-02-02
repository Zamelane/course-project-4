using ClientApp.Src.Storage;
using System.Diagnostics;

namespace ClientApp.Src.Views;

public partial class BrowsePage : ContentPage
{
    public BrowsePage()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Debug.WriteLine("Типа нажал");
    }
}