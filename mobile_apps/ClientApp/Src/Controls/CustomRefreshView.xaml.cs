using System.Windows.Input;

namespace ClientApp.Src.Controls;

public partial class CustomRefreshView : ContentView
{
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command), typeof(ICommand), typeof(CustomRefreshView), default, BindingMode.TwoWay
    );

    public CustomRefreshView()
    {
        InitializeComponent();
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}