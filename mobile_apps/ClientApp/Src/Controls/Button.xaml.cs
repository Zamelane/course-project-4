using System.Windows.Input;

namespace ClientApp.Src.Controls;

public partial class Button : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(Button), string.Empty, BindingMode.TwoWay
    );

    public static readonly BindableProperty IsVisibleArrowProperty = BindableProperty.Create(
        nameof(IsVisibleArrow), typeof(bool), typeof(Button), false, BindingMode.TwoWay
    );

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(Button), default, BindingMode.TwoWay
    );

    public Button()
    {
        InitializeComponent();
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public bool IsVisibleArrow
    {
        get => (bool)GetValue(IsVisibleArrowProperty);
        set => SetValue(IsVisibleArrowProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}