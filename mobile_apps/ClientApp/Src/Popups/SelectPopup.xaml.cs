using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using MauiContentButton;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ClientApp.Src.Popups;

public partial class SelectPopup : Popup
{
    private readonly ObservableCollection<object> _selectedElements = [];

    public bool IsMultiple { get; } = false;
    public ICommand CloseCommand { get; set; }
    public ICommand ApplyCommand { get; set; }

    public SelectPopup(dynamic? element)
    {
        if (element != null)
        {
            var elementType = element.GetType();

            // Проверяем, является ли тип ObservableCollection<>
            if (elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
            {
                IsMultiple = true;
                _selectedElements = new ObservableCollection<object>((IEnumerable<object>)element);
            }
            else
                _selectedElements.Add(element);
        }

        Debug.WriteLine(_selectedElements.Count);
        Debug.WriteLine(IsMultiple);
        Debug.WriteLine("==========");

        CloseCommand = new RelayCommand(() =>
        {
            Close();
        });

        ApplyCommand = new RelayCommand(() =>
        {
            CloseAsync(_selectedElements);
        });

        BindingContext = this;
        InitializeComponent();
    }

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
        typeof(DataTemplate), typeof(SelectPopup));
    public void TapElementOne(object? s)
    {
        Debug.WriteLine("321");
        if (s is not View v)
            return;
        var currentItem = v.BindingContext;
        if (!IsMultiple)
        {
            CloseAsync(currentItem);
            return;
        }
    }
    public void TapElementMore(object? s, Image? i)
    {
        Debug.WriteLine("123");
        if (s is not View v || i is null)
            return;

        var currentItem = v.BindingContext;

        if (_selectedElements.Contains(currentItem))
        {
            _selectedElements.Remove(currentItem);
            i.IsVisible = false;
        }
        else
        {
            _selectedElements.Add(v.BindingContext);
            i.IsVisible = true;
        }
        //CloseAsync(v.BindingContext);
    }
    public DataTemplate ItemTemplate
    {
        // get => (DataTemplate)GetValue(ItemTemplateProperty);
        get => new DataTemplate()
        {
            LoadTemplate = () =>
            {
                var dt = (DataTemplate)GetValue(ItemTemplateProperty);
                var template = dt.LoadTemplate();

                if (template is View v)
                {
                    //var tgr = new TapGestureRecognizer();

                    if (!IsMultiple)
                    {
                        return new ContentButton()
                        {
                            Content = v,
                            Command = new Command(() => {
                                Debug.WriteLine("Нажато, на удивление, одиночное");
                                TapElementOne(v);
                            })
                        };
                    }

                    var g = new Grid()
                    {
                        ColumnDefinitions = [
                            new(new GridLength(1, GridUnitType.Star)),
                            new(new GridLength(1, GridUnitType.Auto))
                        ],
                        ColumnSpacing = 5,
                        InputTransparent = true
                    };

                    var checkbox = new Image()
                    {
                        Source = "check.png",
                        WidthRequest = 24,
                        HeightRequest = 24,
                        IsVisible = false
                    };


                    checkbox.Loaded += (s, e) =>
                    {
                        if (s is View v)
                        {
                            v.IsVisible = _selectedElements.Contains(v.BindingContext);
                        }
                    };

                    g.SetColumn(checkbox, 1);
                    g.SetColumn(v, 0);

                    g.Children.Add(checkbox);
                    g.Children.Add(v);

                    return new ContentButton()
                    {
                        Content = g,
                        Command = new Command(() => {
                            Debug.WriteLine("Нажато, на удивление");
                            TapElementMore(g, checkbox);
                        })
                    };
                }

                Debug.WriteLine("Рисую оригинальный template для select'a");

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

    //public new async void Close(object? result = null)
    //{
        
    //}
}