using RequestsLibrary.Responses.Api.News;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;

namespace ClientApp.Src.Components;

public partial class DefaultNewsCard : ContentView
{
	public DefaultNewsCard()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty FilteredNewsProperty = BindableProperty.Create(
        nameof(FilteredNews), typeof(FilteredNewsResponse), typeof(DefaultNewsCard), null, BindingMode.TwoWay
    );

    public FilteredNewsResponse? FilteredNews
    {
        get => (FilteredNewsResponse?)GetValue(FilteredNewsProperty);
        set => SetValue(FilteredNewsProperty, value);
    }
}