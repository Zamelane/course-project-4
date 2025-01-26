using RequestsLibrary.Models;

namespace ClientApp.Src.Components;

public partial class DefaultNewsCard : ContentView
{
    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(MinNews), typeof(DefaultNewsCard), null, BindingMode.TwoWay
    );

    public DefaultNewsCard()
    {
        InitializeComponent();
    }

    public MinNews? FilteredNews
    {
        get => (MinNews?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }
}