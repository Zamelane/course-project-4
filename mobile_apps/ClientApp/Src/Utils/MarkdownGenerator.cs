using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace ClientApp.Src.Utils;
public static class MarkdownGenerator
{
    public static IView GenerateElement(HeadingBlock heading)
    {
        (var element, var status) = GenerateTextContent(heading.Inline);

        if (status)
        {
            SetStyle(element, "H" + heading.Level.Clamp(1, 3));
        }

        return element;
    }
    public static IView GenerateElement(ParagraphBlock paragraph)
    {
        (var element, var status) = GenerateTextContent(paragraph.Inline);

        if (status)
        {
            SetStyle(element, "P");
        }

        ((Label)element).TextColor = Colors.Red;

        return element;
    }

    /* ===ШАБЛОНЫ МЕЛКИХ КОМПОНЕНТОВ=== */
    public static (IView, bool) GenerateTextContent(ContainerInline? conteinerInline)
    {
        var tb = new FormattedString();

        if (conteinerInline is null)
            return (GenerateElement(), false);

        foreach (var inline in conteinerInline)
        {
            if (inline is LiteralInline literal)
                tb.Spans.Add(GenerateSpan(literal.Content.ToString()));
            else if (inline is LinkInline link)
                tb.Spans.Add(link.Title is not null ? GenerateSpan(link.Title) : GenerateSpan(link.Url ?? "[no_link]"));
        }

        var element = new Label() { FormattedText = tb };

        return (element, true);
    }
    public static IView GenerateElement(string text = $"[Не могу отрисовать элемент]", MarkdownObject? node = null) => new Label() { Text = text };
    public static IView GenerateElement(MarkdownObject node) => new Label() { Text = $"[{node.GetType().Name}]" };

    /* ===СТИЛИЗАЦИЯ И ОФОРМЛЕНИЕ=== */
    private static Span GenerateSpan(string text, string? styleKey = null) => new Span() { Text = text };
    private static void SetStyle(Element element, string key) => element.SetDynamicResource(VisualElement.StyleProperty, key);
    private static void SetStyle(IView element, string key) => SetStyle((Element)element, key);

    /* ===ВСПОМОГАТЕЛЬНОЕ===*/
    private static int Clamp(this int value, int min, int max) => min > value 
        ? min 
        : max < value 
            ? max
            : value;
}
