using System.Diagnostics;

namespace ClientApp.Src.Components;

public partial class CategoryCard : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(CategoryCard), string.Empty);

    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(nameof(IconSource), typeof(string), typeof(CategoryCard), string.Empty);

    public static readonly BindableProperty CardColorProperty =
        BindableProperty.Create(nameof(CardColor), typeof(string), typeof(CategoryCard), string.Empty);

    public CategoryCard()
    {
        InitializeComponent();
    }

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

    public Color CardColor
    {
        get => Color.FromArgb((string)GetValue(CardColorProperty));
        set => SetValue(CardColorProperty, value);
    }

    private void ContentButton_Clicked(object sender, EventArgs e)
    {
        Debug.WriteLine("Категория нажата");
    }
}