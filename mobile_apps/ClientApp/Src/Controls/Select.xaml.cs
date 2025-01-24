using ClientApp.Src.Popups;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace ClientApp.Src.Controls;

public partial class Select : ContentView
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), typeof(int), typeof(Select), 5, BindingMode.OneWayToSource
    );

    public static readonly BindableProperty IsMultipleProperty = BindableProperty.Create(
        nameof(IsMultiple), typeof(bool), typeof(Select), false, BindingMode.TwoWay
    );
    public bool IsMultiple
    {
        get => (bool)GetValue(IsMultipleProperty);
        set => SetValue(IsMultipleProperty, value);
    }

    // <------ Поля для прицепа
    //public static readonly BindableProperty MultiplyItemsSelectedProperty = BindableProperty.Create(
    //    nameof(MultiplyItemsSelected), typeof(ObservableCollection<object>), typeof(Select), new ObservableCollection<object>(), BindingMode.TwoWay);
    //public ObservableCollection<object> MultiplyItemsSelected
    //{
    //    get => (ObservableCollection<object>)GetValue(MultiplyItemsSelectedProperty);
    //    set => SetValue(MultiplyItemsSelectedProperty, value);
    //}

    public static readonly BindableProperty ItemsSelectedProperty = BindableProperty.Create(
        nameof(ItemsSelected), typeof(object), typeof(Select), null, BindingMode.TwoWay);
    public object? ItemsSelected
    {
        get => (object?)GetValue(ItemsSelectedProperty);
        set => SetValue(ItemsSelectedProperty, value);
    }

    public static readonly BindableProperty SelectedTextProperty = BindableProperty.Create(
        nameof(SelectedText), typeof(string), typeof(Select), null, BindingMode.TwoWay
    );
    public string SelectedText
    {
        get
        {
            var text = (string)GetValue(SelectedTextProperty);
            return String.IsNullOrEmpty(text) ? "Не выбрано" : text;
        }
        set => SetValue(SelectedTextProperty, value);
    }
    // ------> ...

    public Select()
    {
        InitializeComponent();
    }

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    // <------ Проброс
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
        typeof(DataTemplate), typeof(Select));
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), typeof(object), typeof(Select), default, BindingMode.TwoWay
    );
    public object ItemsSource
    {
        get => (object)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(Select), default, BindingMode.TwoWay
    );
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    // ------> ...

    private void Select_Tapped(object sender, TappedEventArgs e)
    {
        _ =  OpenPopup();
    }

    private async Task OpenPopup()
    {
        Debug.WriteLine(JsonSerializer.Serialize(ItemsSelected));

        var popup = new SelectPopup(ItemsSelected)
        {
            ItemsSource = ItemsSource,
            ItemTemplate = ItemTemplate,
            Title = Title
        };

        object? result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        if (result is null)
            return;

        //if (result is null)
        //{
        //    //ItemSelected = null;
        //    MultiplyItemsSelected.Clear();
        //}

        if (result is ObservableCollection<object> collection)
        {
            if (IsMultiple)
                ItemsSelected = collection;
            else ItemsSelected = collection.FirstOrDefault();
            return;
        }

        if (result is object)
        {
            ItemsSelected = result;
        }
    }
}