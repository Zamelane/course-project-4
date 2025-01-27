using ClientApp.Src.Utils;
using Markdig;
using Markdig.Syntax;

namespace ClientApp.Src.Controls;

public partial class MarkdownViewer : ContentView
{
	public MarkdownViewer()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty MarkdownTextProperty = BindableProperty.Create(
        nameof(MarkdownText), typeof(string), typeof(IconEntry), string.Empty, BindingMode.TwoWay, propertyChanged: OnMarkdownTextChanged
    );
    public string MarkdownText
    {
        get => (string)GetValue(MarkdownTextProperty);
        set => SetValue(MarkdownTextProperty, value);
    }

    private static void OnMarkdownTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //string md_text = (string)newValue;

        string md_text = $@"# What is an Interior Designer?

Donec at elit a sem tincidunt interdum in sed quam. In fringilla, massa eget sagittis viverra, sapien tellus mollis libero, nec ullamcorper purus quam ac turpis. ";

        var control = (MarkdownViewer) bindable;

        control.MD_layout.Children.Clear();
        control.GenerateMarkdownDocument(md_text, control.MD_layout);
    }

    private void GenerateMarkdownDocument(string md_text, StackLayout layout)
    {
        var pipeline = new MarkdownPipelineBuilder().Build();
        var document = Markdown.Parse(md_text, pipeline);

        foreach (var node in document)
        {
            layout.Children.Add(ProcessNode(node));
        }


        layout.Children.Add(new Label()
        {
            Text = "Test",
            TextColor = Colors.Blue
        });
    }

    private IView ProcessNode(MarkdownObject node)
    {
        switch (node)
        {
            case HeadingBlock heading:
                return MarkdownGenerator.GenerateElement(heading);
            case ParagraphBlock paragraph:
                return MarkdownGenerator.GenerateElement(paragraph);
        }
        return MarkdownGenerator.GenerateElement(node);
    }
}