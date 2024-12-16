namespace ClientApp.Src.Components;

public partial class CategoryCard : ContentView
{
	public CategoryCard()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CategoryCard), String.Empty);
    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(nameof(IconSource), typeof(string), typeof(CategoryCard), String.Empty);
    public static readonly BindableProperty CardColorProperty = BindableProperty.Create(nameof(CardColor), typeof(string), typeof(CategoryCard), String.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }
    public string CardColor
    {
        get => (string)GetValue(CardColorProperty);
        set => SetValue(CardColorProperty, value);
    }
}