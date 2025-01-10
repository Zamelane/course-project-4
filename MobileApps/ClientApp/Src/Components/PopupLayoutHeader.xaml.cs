using RequestsLibrary.Responses.Api.News;

namespace ClientApp.Src.Components;

public partial class PopupLayoutHeader : ContentView
{
    public enum IconType
    {
        Delete,
        Info
    }
	public PopupLayoutHeader()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon), typeof(IconType), typeof(PopupLayoutHeader), null, BindingMode.TwoWay
    );
    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
        nameof(IconSource), typeof(string), typeof(PopupLayoutHeader), Source(IconType.Delete), BindingMode.TwoWay
    );
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(PopupLayoutHeader), "Title", BindingMode.TwoWay
    );
    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(PopupLayoutHeader), "Description", BindingMode.TwoWay
    );

    public IconType? Icon
    {
        get => (IconType?)GetValue(IconProperty);
        set {
            SetValue(IconProperty, value);
            IconSource = Source(value ?? IconType.Delete);
        }
    }
    public String? IconSource
    {
        get => (String?)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public String? Title
    {
        get => (String?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public String? Description
    {
        get => (String?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    private static string Source(IconType t)
    {
        switch (t)
        {
            case IconType.Info:
                return "info_icon.png";
            case IconType.Delete:
                return "delete_icon.png";
        }

        return "info_icon.png";
    }
}