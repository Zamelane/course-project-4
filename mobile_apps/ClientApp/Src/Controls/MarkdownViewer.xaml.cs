using ClientApp.Src.Utils;
using Markdig;
using Markdig.Syntax;
using System.Diagnostics;

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

        /*string md_text = $@"# What is an Interior Designer?

Donec at elit a sem tincidunt interdum in sed quam. In fringilla, massa eget sagittis viverra, sapien tellus mollis libero, nec ullamcorper purus quam ac turpis.

# What Does an Interior Designer Do?

Integer id est ut nibh posuere feugiat sit amet at turpis. Ut turpis quam, posuere eu malesuada quis, dapibus nec ligula. Etiam porta lacus nec luctus luctus. Nullam ut vulputate nunc, eu ultrices velit. Morbi sagittis interdum arcu eu feugiat. Nam interdum justo risus.

![The San Juan Mountains are beautiful!](https://cdn.zmln.ru/Rectangle%208.png ""San Juan Mountains"")

# 1. Educate your eye. You can hone your eye at any age, whether you're just entering design school or coming to interior design later in life.

Integer pulvinar lacus ac consequat dapibus. Aenean tristique accumsan nunc et lobortis. Nullam a lorem ligula. Pellentesque sit amet pretium ligula, in ullamcorper ligula.

Morbi rutrum sagittis augue. Donec augue lorem, gravida ac diam a, accumsan egestas mauris. Fusce ut tortor nec nulla scelerisque vulputate. Duis pharetra pretium felis vitae interdum. Quisque et erat risus. Donec a ante felis. Aliquam id felis hendrerit elit elementum bibendum. Quisque turpis arcu, aliquam ac lectus et, fringilla semper sem. Nulla congue vitae orci ac aliquet. Aliquam eget blandit tellus, vel sagittis mi. Nam leo ipsum, scelerisque a turpis ac, feugiat rhoncus erat. Nulla ante orci, accumsan sed mauris vel, suscipit porta eros.
        
";*/

        string md_text = (string)newValue;

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