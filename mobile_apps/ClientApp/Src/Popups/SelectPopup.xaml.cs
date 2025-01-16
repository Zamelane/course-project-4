using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace ClientApp.Src.Popups;

public partial class SelectPopup : Popup
{
    private readonly ObservableCollection<object> _selectedElements = [];
    private readonly bool _isMultiple;

    public SelectPopup(object? selectedElement) : this(false) {
        if (selectedElement is null)
            return;
        _selectedElements.Add(selectedElement);
    }

    public SelectPopup(ObservableCollection<object> selectedElements) : this(true) => _selectedElements = selectedElements;

    public SelectPopup(bool isMultiple)
    {
        _isMultiple = isMultiple;
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
                    {
                        if (!_isMultiple)
                        {
                            CloseAsync(v.BindingContext);
                            return;
                        }

                        //_selectedElements.Add(v.BindingContext);
                        //CloseAsync(v.BindingContext);
                    }
                };

                if (template is View v)
                {
                    //v.GestureRecognizers.Add(tgr);
                    var g = new Grid()
                    {
                        ColumnDefinitions = [
                            new(new GridLength(1, GridUnitType.Star)),
                            new(new GridLength(1, GridUnitType.Auto))
                        ],
                        ColumnSpacing = 5
                    };

                    var checkbox = new Image()
                    {
                        Source = "check.png",
                        WidthRequest = 24,
                        HeightRequest = 24
                    };

                    g.SetColumn(checkbox, 1);
                    g.SetColumn(v, 0);

                    g.Children.Add(checkbox);
                    g.Children.Add(v);

                    return g;
                }

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