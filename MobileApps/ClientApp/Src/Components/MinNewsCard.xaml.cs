using RequestsLibrary.Responses.Api.News;

namespace ClientApp.Src.Components;

public partial class MinNewsCard : ContentView
{
    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(FilteredNewsResponse), typeof(DefaultNewsCard), null, BindingMode.TwoWay
    );

    public MinNewsCard()
    {
        InitializeComponent();
    }

    public FilteredNewsResponse? FilteredNews
    {
        get => (FilteredNewsResponse?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }
}