namespace ClientApp.Src.Components;

public partial class IconEntry : ContentView
{
	public IconEntry()
	{
		InitializeComponent();
	}

	
	public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(IconEntry), String.Empty);
    public static readonly BindableProperty IconSourceProperty	    = BindableProperty.Create(nameof(IconSource), typeof(string), typeof(IconEntry), String.Empty);

	public string PlaceholderText
	{
		get => (string)GetValue(PlaceholderTextProperty);
		set => SetValue(PlaceholderTextProperty, value);
	}
    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }
}