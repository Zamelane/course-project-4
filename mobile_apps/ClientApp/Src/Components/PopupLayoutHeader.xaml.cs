using System.Windows.Input;

namespace ClientApp.Src.Components;

public partial class PopupLayoutHeader : ContentView
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(PopupLayoutHeader), default, BindingMode.TwoWay
    );
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public enum IconType
    {
        Delete,
        Protected
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

    public PopupLayoutHeader()
    {
        InitializeComponent();
        IconSource = Source((IconType)GetValue(IconProperty));
    }

    public IconType? Icon
    {
        get => (IconType?)GetValue(IconProperty);
        set
        {
            SetValue(IconProperty, value);
            IconSource = Source(value ?? IconType.Delete);
        }
    }

    public string? IconSource
    {
        get => (string?)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string? Title
    {
        get => (string?)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string? Description
    {
        get => (string?)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    private static string Source(IconType t)
    {
        switch (t)
        {
            case IconType.Protected:
                return "protected_icon.png";
            case IconType.Delete:
                return "delete_icon.png";
        }

        return "protected_icon.png";
    }
}