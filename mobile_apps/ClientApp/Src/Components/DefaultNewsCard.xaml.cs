using RequestsLibrary.Models;

namespace ClientApp.Src.Components;

public partial class DefaultNewsCard : ContentView
{
    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(News), typeof(DefaultNewsCard), null, BindingMode.TwoWay
    );

    public DefaultNewsCard()
    {
        InitializeComponent();
    }

    public News? FilteredNews
    {
        get => (News?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }
}