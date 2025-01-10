using ClientApp.Src.Utils;

namespace ClientApp.Src.Components;

public partial class PopupLayoutFooter : ModifyContentView
{
    public static readonly BindableProperty BeforeTemplateProperty = BindableProperty.Create(nameof(BeforeTemplate),
        typeof(ControlTemplate), typeof(PopupLayoutFooter));

    public static readonly BindableProperty AfterTemplateProperty = BindableProperty.Create(nameof(AfterTemplate),
        typeof(ControlTemplate), typeof(PopupLayoutFooter));

    public PopupLayoutFooter()
    {
        InitializeComponent();
    }

    public ControlTemplate BeforeTemplate
    {
        get => (ControlTemplate)GetValue(BeforeTemplateProperty);
        set => SetValue(BeforeTemplateProperty, value);
    }

    public ControlTemplate AfterTemplate
    {
        get => (ControlTemplate)GetValue(AfterTemplateProperty);
        set => SetValue(AfterTemplateProperty, value);
    }
}