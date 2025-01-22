using ClientApp.Src.Utils;
using System.Diagnostics;
using System.Windows.Input;

namespace ClientApp.Src.Controls;

public partial class IconEntry : ContentView
{
    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText), typeof(string), typeof(IconEntry)
    );

    public static readonly BindableProperty PlaceholderTextProperty = BindableProperty.Create(
        nameof(PlaceholderText), typeof(string), typeof(IconEntry), string.Empty
    );

    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
        nameof(IconSource), typeof(string), typeof(IconEntry), string.Empty
    );

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(IconEntry), string.Empty, BindingMode.TwoWay
    );

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        nameof(IsPassword), typeof(bool), typeof(IconEntry), false
    );

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius), typeof(int), typeof(IconEntry), 5, BindingMode.OneWayToSource
    );

    private Brush _beforeBordeColor;

    public IconEntry()
    {
        InitializeComponent();
    }

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public event EventHandler Completed;

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        Completed?.Invoke(null, EventArgs.Empty);
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        setEntryBorderColor("Gray500");
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        setEntryBorderColor(_beforeBordeColor);
    }

    private void setEntryBorderColor(dynamic resourceName)
    {
        var entryBorder = GetTemplateChild("EntryBorder");
        var rd = App.Current.Resources.MergedDictionaries.First();


        if (entryBorder is not Border fr)
            return;

        _beforeBordeColor = ((Border)entryBorder).Stroke;

        Brush color = resourceName is Brush ? resourceName : (Color)rd[resourceName];
        ((Border)entryBorder).Stroke = color;
    }

    private void Entry_Tap(object sender, TappedEventArgs e)
    {
        var entry = GetTemplateChild("CustomEntry");

        if (entry is Entry ce)
        {
            ce.Focus();
            Debug.WriteLine("������������ �� " + ce.Id);
        }

        Debug.WriteLine("���� �������");
    }

    // *** ЛОГИКА ПОИСКА ** //
    public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(
        nameof(SearchCommand), typeof(ICommand), typeof(IconEntry), null, BindingMode.TwoWay
    );
    public ICommand SearchCommand
    {
        get => (ICommand)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }
    private Debouncer _debouncer = new(1000);
    private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchCommand is null)
            return;
        _debouncer.Debounce(() => SearchCommand.Execute(null));
    }
}