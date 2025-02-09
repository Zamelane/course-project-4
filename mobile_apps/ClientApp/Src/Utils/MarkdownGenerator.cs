using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using RequestsLibrary;
using System.Diagnostics;

namespace ClientApp.Src.Utils;
public static class MarkdownGenerator
{
    private enum BlockType
    {
        Heading,
        Paragraph
    }
    public static IView GenerateElement(HeadingBlock heading)
    {
        (var element, var status) = GenerateTextContent(
            heading.Inline,
            "H" + heading.Level.Clamp(1, 3),
            "Gray900"
        );

        Debug.WriteLine("ВЫШЕЛ ИЗ ГЕНЕРАТОРА H");


        if (!status)
            return GenerateElement((MarkdownObject)heading);

        return element;
    }
    public static IView GenerateElement(ParagraphBlock paragraph)
    {
        (var element, var status) = GenerateTextContent(paragraph.Inline, "P", "Gray600");
        Debug.WriteLine("ВЫШЕЛ ИЗ ГЕНЕРАТОРА P");
        if (!status)
            return GenerateElement((MarkdownObject)paragraph);

        return element;
    }

    /* ===ШАБЛОНЫ МЕЛКИХ КОМПОНЕНТОВ=== */
    private static (IView, bool) GenerateTextContent(ContainerInline? conteinerInline, string resourceKey, string colorResourceKey, BlockType t = BlockType.Paragraph)
    {
        List<dynamic> tb = [new FormattedString()];

        if (conteinerInline is null)
            return (GenerateElement(), false);

        foreach (var inline in conteinerInline)
        {
            if (inline is LiteralInline literal)
            {
                Debug.WriteLine(literal.Content.ToString());
                //var tbf = tb.First();
                tb.First().Spans.Add(GenerateSpan(literal.Content.ToString()));
            }
            else if (inline is LinkInline link)
            {
                if (link.IsImage)
                {
                    string url = Fetcher.Config.GetStorageUrl() + link?.Url?.Replace(":api:storage", "");
                    tb.Add(new Image()
                    {
                        Source = url
                    });
                    Debug.WriteLine("Image url: " + url);
                    tb.Add(new FormattedString());
                } else
                {
                    tb.First().Spans.Add(link.Title is not null ? GenerateSpan(link.Title) : GenerateSpan(link.Url ?? "[no_link]"));
                }
            } 
            else if (inline is LineBreakInline lbi)
            {
                tb.Add(new FormattedString());
            }
            else
            {
                Debug.WriteLine(inline);
                return (default, false);
            }
        }

        var sl = new StackLayout();

        foreach (var e in tb)
        {
            if (e is FormattedString fs && fs.Spans.Count > 0){
                var label = new Label() { FormattedText = fs };

                SetStyle((IView)label, resourceKey);
                SetColorFromResourceToSpans(label.FormattedText, colorResourceKey);
                SetFontToSpans(label.FormattedText, t == BlockType.Heading ? "Nunito-SemiBold" : "Nunito");

                sl.Children.Add(label);
            }
            else if (e is Image i){
                SetStyle((IView)i, "I");
                sl.Children.Add(i);
            }
        }

        return (sl, true);
    }
    public static IView GenerateElement(string text = "[Не могу отрисовать элемент]") => new Label() { Text = text };
    public static IView GenerateElement(MarkdownObject node) => new Label() { Text = $"[{node.GetType().Name}]" };

    /* ===СТИЛИЗАЦИЯ И ОФОРМЛЕНИЕ=== */
    private static Span GenerateSpan(string text, string? styleKey = null) => new Span() { Text = text };
    private static void SetStyle(
        Element element,
        string key,
        BindableProperty? bp = null
    ) => element.SetDynamicResource(bp ?? VisualElement.StyleProperty, key);
    private static void SetStyle(
        IView element,
        string key,
        BindableProperty? bp = null
    ) => SetStyle((Element)element, key, bp);
    private static void SetColorFromResourceToSpans(FormattedString fs, string resourceKey)
    {
        foreach (var span in fs.Spans)
            SetStyle(span, resourceKey, Label.TextColorProperty);
    }
    private static void SetFontToSpans(FormattedString fs, string font = "Nunito")
    {
        foreach (var span in fs.Spans)
            span.FontFamily = font;
    }

    /* ===ВСПОМОГАТЕЛЬНОЕ===*/
    private static int Clamp(this int value, int min, int max) => min > value 
        ? min 
        : max < value 
            ? max
            : value;
}
