using System.Windows.Input;

namespace ClientApp.Src.Components;

public partial class TitledContentTemplate : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(TitledContentTemplate), string.Empty);

    public static readonly BindableProperty ShowMoreProperty =
        BindableProperty.Create(nameof(ShowMore), typeof(bool), typeof(TitledContentTemplate), false);

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(TitledContentTemplate), default, BindingMode.TwoWay
    );

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

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