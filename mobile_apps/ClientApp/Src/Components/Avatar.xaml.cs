namespace ClientApp.Src.Components;

public partial class Avatar : ContentView
{
	public Avatar()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty AvatarDataProperty = BindableProperty.Create(
        nameof(AvatarData), typeof(RequestsLibrary.Models.Image), typeof(Avatar), null, BindingMode.TwoWay
    );

    public RequestsLibrary.Models.Image? AvatarData
    {
        get => (RequestsLibrary.Models.Image?)GetValue(AvatarDataProperty);
        set => SetValue(AvatarDataProperty, value);
    }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), typeof(int), typeof(Avatar), 5, BindingMode.TwoWay
    );

    public int? CornerRadius
    {
        get => (int?)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    public static readonly BindableProperty EmailProperty = BindableProperty.Create(
        nameof(Email), typeof(string), typeof(Avatar), "test@mail.ru", BindingMode.TwoWay
    );

    public string? Email
    {
        get => (string?)GetValue(EmailProperty);
        set => SetValue(EmailProperty, value);
    }

}