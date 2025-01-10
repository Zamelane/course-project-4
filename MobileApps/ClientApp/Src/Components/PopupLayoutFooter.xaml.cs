using ClientApp.Src.Utils;
using System.Diagnostics;

namespace ClientApp.Src.Components;

public partial class PopupLayoutFooter : ModifyContentView
{
	public PopupLayoutFooter()
	{
		InitializeComponent();
    }

    public static readonly BindableProperty BeforeTemplateProperty = BindableProperty.Create(nameof(BeforeTemplate), typeof(ControlTemplate), typeof(PopupLayoutFooter), default);

    public ControlTemplate BeforeTemplate
    {
        get => (ControlTemplate)GetValue(BeforeTemplateProperty);
        set
        {
            SetValue(BeforeTemplateProperty, value);
        }
    }

    public static readonly BindableProperty AfterTemplateProperty = BindableProperty.Create(nameof(AfterTemplate), typeof(ControlTemplate), typeof(PopupLayoutFooter), default);

    public ControlTemplate AfterTemplate
    {
        get => (ControlTemplate)GetValue(AfterTemplateProperty);
        set => SetValue(AfterTemplateProperty, value);
    }
}