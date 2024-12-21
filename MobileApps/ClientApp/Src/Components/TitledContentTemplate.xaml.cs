namespace ClientApp.Src.Components;

public partial class TitledContentTemplate : ContentView
{
	public TitledContentTemplate()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(TitledContentTemplate), String.Empty);
    public static readonly BindableProperty ShowMoreProperty = BindableProperty.Create(nameof(ShowMore), typeof(bool), typeof(TitledContentTemplate), false);
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public bool ShowMore
    {
        get => (bool)GetValue(ShowMoreProperty);
        set => SetValue(ShowMoreProperty, value);
    }
}