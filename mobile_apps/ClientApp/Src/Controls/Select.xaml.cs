using System.Collections.ObjectModel;
using ClientApp.Src.Popups;
using CommunityToolkit.Maui.Views;

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
    public static readonly BindableProperty MultiplyItemsSelectedProperty = BindableProperty.Create(
        nameof(MultiplyItemsSelected), typeof(ObservableCollection<object>), typeof(SelectPopup), new ObservableCollection<object>(), BindingMode.TwoWay);
    public ObservableCollection<object> MultiplyItemsSelected
    {
        get => (ObservableCollection<object>)GetValue(MultiplyItemsSelectedProperty);
        set => SetValue(MultiplyItemsSelectedProperty, value);
    }

    public static readonly BindableProperty ItemSelectedProperty = BindableProperty.Create(
        nameof(ItemSelected), typeof(object), typeof(SelectPopup), null, BindingMode.TwoWay);
    public object? ItemSelected
    {
        get => (object?)GetValue(ItemSelectedProperty);
        set => SetValue(ItemSelectedProperty, value);
    }

    public static readonly BindableProperty SelectedTextProperty = BindableProperty.Create(
        nameof(SelectedText), typeof(string), typeof(SelectPopup), null, BindingMode.TwoWay
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
        var popup = new SelectPopup(IsMultiple ? MultiplyItemsSelected : ItemSelected)
        {
            ItemsSource = ItemsSource,
            ItemTemplate = ItemTemplate,
            Title = Title
        };

        object? result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        //if (result is null)
        //{
        //    //ItemSelected = null;
        //    MultiplyItemsSelected.Clear();
        //}

        if (result is ObservableCollection<object> collection)
        {
            if (IsMultiple)
                MultiplyItemsSelected = collection;
            else ItemSelected = collection.FirstOrDefault();
            return;
        }

        if (result is object)
        {
            ItemSelected = result;
            MultiplyItemsSelected.Clear();
            MultiplyItemsSelected.Add(result);
        }
    }
}