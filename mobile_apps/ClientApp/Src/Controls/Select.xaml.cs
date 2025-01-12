namespace ClientApp.Src.Controls;

public partial class Select : ContentView
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), typeof(int), typeof(IconEntry), 5, BindingMode.OneWayToSource
    );

    public Select()
    {
        InitializeComponent();
    }

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    private void Select_Tapped(object sender, TappedEventArgs e)
    {
        //rl.OpenPopup(new Popup());
    }
}