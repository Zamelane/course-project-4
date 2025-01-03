using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClientApp.Src.Controls;

public partial class Button : ContentView
{
	public Button()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(IconEntry), string.Empty, BindingMode.TwoWay
    );
    public static readonly BindableProperty IsVisibleArrowProperty = BindableProperty.Create(
        nameof(IsVisibleArrow), typeof(bool), typeof(IconEntry), false, BindingMode.TwoWay
    );
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(IconEntry), default, BindingMode.TwoWay
    );
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