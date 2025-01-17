namespace ClientApp.Src.Controls;

public partial class ResizableCollectionView : ContentView
{
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
        typeof(DataTemplate), typeof(ResizableCollectionView));

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), typeof(object), typeof(ResizableCollectionView), default, BindingMode.TwoWay
    );

    public static readonly BindableProperty CriticalWindowSizeProperty = BindableProperty.Create(
        nameof(CriticalWindowSize), typeof(int), typeof(ResizableCollectionView), 350, BindingMode.TwoWay
    );

    public static readonly BindableProperty ElementCriticalSizeProperty = BindableProperty.Create(
        nameof(ElementCriticalSize), typeof(int), typeof(ResizableCollectionView), 220, BindingMode.TwoWay
    );

    public ResizableCollectionView()
    {
        InitializeComponent();
    }

    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public object ItemsSource
    {
        get => (object)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public int CriticalWindowSize
    {
        get => (int)GetValue(CriticalWindowSizeProperty);
        set => SetValue(CriticalWindowSizeProperty, value);
    }

    public int ElementCriticalSize
    {
        get => (int)GetValue(ElementCriticalSizeProperty);
        set => SetValue(ElementCriticalSizeProperty, value);
    }

    private void ContentView_SizeChanged(object sender, EventArgs e)
    {
        var gil = GetTemplateChild("gil");

        if (gil is not GridItemsLayout)
            return;

        var currentSize = ((ContentView)sender).Window.Width;

        var spans = Convert.ToInt32(Math.Round(currentSize / ElementCriticalSize, 0));

        if (spans < 1 || currentSize <= CriticalWindowSize)
            spans = 1;

        ((GridItemsLayout)gil).Span = spans;
    }
}