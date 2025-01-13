using System.Diagnostics;
using CommunityToolkit.Maui.Views;

namespace ClientApp.Src.Popups;

public partial class SelectPopup : Popup
{
	public SelectPopup()
	{
		InitializeComponent();
        BindingContext = this;
	}

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
        typeof(DataTemplate), typeof(SelectPopup));
    public DataTemplate ItemTemplate
    {
        // get => (DataTemplate)GetValue(ItemTemplateProperty);
        get => new DataTemplate() {
            LoadTemplate = () => {
                var dt = (DataTemplate)GetValue(ItemTemplateProperty);
                var template = dt.LoadTemplate();

                var tgr = new TapGestureRecognizer();

                tgr.Tapped += (s, e) =>
                {
                    if (s is View v)
                        CloseAsync(v.BindingContext);
                };

                if (template is View v)
                    v.GestureRecognizers.Add(tgr);

                return template;
            }
        };
        set => SetValue(ItemTemplateProperty, value);
    }
    
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), typeof(object), typeof(SelectPopup), default, BindingMode.TwoWay
    );
    public object ItemsSource
    {
        get => (object)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(SelectPopup), default, BindingMode.TwoWay
    );
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}