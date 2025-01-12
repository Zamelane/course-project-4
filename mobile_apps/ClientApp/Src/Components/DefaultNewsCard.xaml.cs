using RequestsLibrary.Responses.Api.News;

namespace ClientApp.Src.Components;

public partial class DefaultNewsCard : ContentView
{
    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(FilteredNewsResponse), typeof(DefaultNewsCard), null, BindingMode.TwoWay
    );

    public DefaultNewsCard()
    {
        InitializeComponent();
    }

    public FilteredNewsResponse? FilteredNews
    {
        get => (FilteredNewsResponse?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }
}