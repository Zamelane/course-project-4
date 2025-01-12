namespace ClientApp.Src.Components;

public partial class TitledContentTemplate : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(TitledContentTemplate), string.Empty);

    public static readonly BindableProperty ShowMoreProperty =
        BindableProperty.Create(nameof(ShowMore), typeof(bool), typeof(TitledContentTemplate), false);

    public TitledContentTemplate()
    {
        InitializeComponent();
    }

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