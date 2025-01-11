using ClientApp.Src.Utils;

namespace ClientApp.Src.Components;

public partial class PopupLayoutFooter : ModifyContentView
{
    public static readonly BindableProperty BeforeTemplateProperty = BindableProperty.Create(nameof(BeforeTemplate),
        typeof(View), typeof(PopupLayoutFooter));

    public static readonly BindableProperty AfterTemplateProperty = BindableProperty.Create(nameof(AfterTemplate),
        typeof(View), typeof(PopupLayoutFooter));

    public PopupLayoutFooter()
    {
        InitializeComponent();
    }

    public View BeforeTemplate
    {
        get => (View)GetValue(BeforeTemplateProperty);
        set => SetValue(BeforeTemplateProperty, value);
    }

    public View AfterTemplate
    {
        get => (View)GetValue(AfterTemplateProperty);
        set => SetValue(AfterTemplateProperty, value);
    }
}