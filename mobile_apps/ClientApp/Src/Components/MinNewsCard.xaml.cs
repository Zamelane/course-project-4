using ClientApp.Src.Views;
using RequestsLibrary.Models;

namespace ClientApp.Src.Components;

public partial class MinNewsCard : ContentView
{
    public MinNewsCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(MinNews), typeof(DefaultNewsCard), null, BindingMode.TwoWay
    );

    public MinNews? FilteredNews
    {
        get => (MinNews?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }

    private void ContentButton_Clicked(object sender, EventArgs e)
    {
        if (FilteredNews is null)
            return;

        Shell.Current.Navigation.PushAsync(new NewsPage(FilteredNews));
    }
}