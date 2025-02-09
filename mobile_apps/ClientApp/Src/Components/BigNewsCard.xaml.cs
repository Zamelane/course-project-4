using ClientApp.Src.Views;
using RequestsLibrary.Models;

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

    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(MinNews), typeof(BigNewsCard), null, BindingMode.TwoWay
    );

    public MinNews? FilteredNews
    {
        get => (MinNews?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }

    private void ContentButton_Clicked(object sender, EventArgs e)
    {
        if (FilteredNews is not null)
            Shell.Current.Navigation.PushAsync(new NewsPage(FilteredNews));
    }
}